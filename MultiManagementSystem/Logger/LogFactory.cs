using MultiManagementSystem.Logger;

public class LogFactory
{
    private readonly IConfiguration _configuration;

    public LogFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ILog CreateLogger(string serviceName, LoggerType loggerType)
    {
        if (loggerType == LoggerType.FileLogger)
        {
            return new FileLogger(serviceName, _configuration);
        }
        else
        {
            return new ConsoleLogger(serviceName);
        }
    }
}

public enum LoggerType
{
    ConsoleLogger = 0,
    FileLogger = 1
}
