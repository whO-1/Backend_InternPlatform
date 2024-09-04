using NLog;
using System;

namespace YourNamespace.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogException(this ILogger logger, Exception ex, string controllerName, string actionName)
        {
            var logEvent = new LogEventInfo(LogLevel.Error, logger.Name, ex.Message)
            {
                Exception = ex
            };

            logEvent.Properties["Controller"] = controllerName;
            logEvent.Properties["Action"] = actionName;

            logger.Log(logEvent);
        }
    }
}