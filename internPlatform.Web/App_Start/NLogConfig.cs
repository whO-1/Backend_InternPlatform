using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;

namespace internPlatform.App_Start
{
    public class NLogConfig
    {
        public LoggingConfiguration Config { get; private set; }
        public FileTarget FileTarget { get; private set; }
        public ConsoleTarget ConsoleTarget { get; private set; }
        //public DatabaseTarget DatabaseTarget { get; private set; }

        public NLogConfig()
        {
            Config = new LoggingConfiguration();

            FileTarget = new FileTarget("fileTarget")
            {
                FileName = $"${{basedir}}/logs/LogFile_{DateTime.Today.ToShortDateString()}.log",
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}"
            };
            ConsoleTarget = new ConsoleTarget("consoleTarget")
            {
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}"
            };

            Config.AddTarget(FileTarget);
            Config.AddTarget(ConsoleTarget);
            //Config.AddTarget(DatabaseTarget);


            Config.AddRule(LogLevel.Trace, LogLevel.Fatal, FileTarget);
            Config.AddRule(LogLevel.Info, LogLevel.Fatal, ConsoleTarget);

            LoadDatabaseConfig();

            LogManager.ThrowExceptions = true;
            LogManager.ThrowConfigExceptions = true;

            LogManager.Configuration = Config;
            //DatabaseTarget = new DatabaseTarget("databaseTarget")
            //{
            //    ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
            //    CommandText = @"
            //        INSERT INTO ErrorLogs (Timestamp, Level, Logger, Message, Exception, CallSite, LineNumber) 
            //        VALUES (@timestamp, @level, @logger, @message, @exception, @callsite, @linenumber)"
            //};
        }

        public void Initialize()
        {
            LogManager.Configuration = Config;

            //DatabaseTarget.Parameters.Add(new DatabaseParameterInfo("@timestamp", "${longdate}"));
            //DatabaseTarget.Parameters.Add(new DatabaseParameterInfo("@level", "${level:uppercase=true}"));
            //DatabaseTarget.Parameters.Add(new DatabaseParameterInfo("@logger", "${logger}"));
            //DatabaseTarget.Parameters.Add(new DatabaseParameterInfo("@message", "${message}"));
            //DatabaseTarget.Parameters.Add(new DatabaseParameterInfo("@exception", "${exception:format=ToString}"));
            //DatabaseTarget.Parameters.Add(new DatabaseParameterInfo("@callsite", "${callsite}"));
            //DatabaseTarget.Parameters.Add(new DatabaseParameterInfo("@linenumber", "${callsite-linenumber}"));


            //Config.AddRule(LogLevel.Error, LogLevel.Fatal, DatabaseTarget);



        }

        private void LoadDatabaseConfig()
        {
            string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NLog.config");

            if (!System.IO.File.Exists(configPath))
            {
                throw new FileNotFoundException($"NLog configuration file not found at {configPath}");
            }
            var xmlConfig = new XmlLoggingConfiguration(configPath, LogManager.LogFactory);

            foreach (var target in xmlConfig.AllTargets)
            {
                Config.AddTarget(target.Name, target);
            }
            foreach (var rule in xmlConfig.LoggingRules)
            {
                Config.LoggingRules.Add(rule);
            }

        }

    }
}