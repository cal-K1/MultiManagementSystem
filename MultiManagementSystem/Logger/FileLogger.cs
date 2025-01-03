using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MultiManagementSystem.Logger
{
    public class FileLogger : LogBase
    {
        private readonly IConfiguration _configuration;

        public FileLogger(string serviceName, IConfiguration configuration)
            : base(serviceName)
        {
            _configuration = configuration;
        }

        public override void LogInternal(string message)
        {
            string logPath = _configuration["AppSettings:logPath"];

            if (string.IsNullOrEmpty(logPath))
            {
                throw new InvalidOperationException("Log path is not configured.");
            }

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            logPath = Path.Combine(logPath, "log.txt");

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.UtcNow}: {message}");
            }
        }
    }
}
