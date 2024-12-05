using Microsoft.Extensions.Configuration;

namespace MultiManagementSystem.Logger
{
    public enum LoggerType
    {
        ConsoleLogger = 0,
        FileLogger = 1
    }

    public static class LogFactory
    {
        public static ILog CreateLogger(string serviceName, LoggerType loggerType, IConfiguration configuration)
        {
            if (loggerType == LoggerType.FileLogger)
            {
                ILog fileLogger = new FileLogger(serviceName, configuration);
                return fileLogger;
            }
            else
            {
                ILog consoleLogger = new ConsoleLogger(serviceName);
                return consoleLogger;
            }
        }
    }
}
