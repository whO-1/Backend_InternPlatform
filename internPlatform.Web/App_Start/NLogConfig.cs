using NLog;
using NLog.Config;
using NLog.Targets;

namespace internPlatform.App_Start
{
    public class NLogConfig
    {
        public LoggingConfiguration Config { get; private set; }
        public FileTarget FileTarget { get; private set; }
        public ConsoleTarget ConsoleTarget { get; private set; }


        public NLogConfig()
        {
            Config = new LoggingConfiguration();
            FileTarget = new FileTarget("fileTarget")
            {
                FileName = "${basedir}/logs/logfile.log",
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}"
            };
            ConsoleTarget = new ConsoleTarget("consoleTarget")
            {
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}"
            };
        }

        public void Initialize()
        {
            Config.AddTarget(FileTarget);
            Config.AddTarget(ConsoleTarget);

            Config.AddRule(LogLevel.Trace, LogLevel.Fatal, FileTarget);
            Config.AddRule(LogLevel.Info, LogLevel.Fatal, ConsoleTarget);

            LogManager.Configuration = Config;
        }
    }
}