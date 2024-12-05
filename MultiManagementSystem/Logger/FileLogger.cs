using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MultiManagementSystem.Logger
{
    public class FileLogger : LogBase
    {
        private readonly string _logPath;
        private readonly IConfiguration _configuration;

        public FileLogger(string serviceName, IConfiguration configuration)
            : base(serviceName)
        {
            _configuration = configuration;
            _logPath = _configuration["AppSettings:LogPath"]
                       ?? throw new ArgumentNullException("LogPath configuration is missing.");
        }

        public override void LogInternal(string message)
        {
            if (!Directory.Exists(_logPath))
            {
                Directory.CreateDirectory(_logPath);
            }

            string logFile = Path.Combine(_logPath, "log.txt");

            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
