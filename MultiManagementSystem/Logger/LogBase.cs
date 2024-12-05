using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiManagementSystem.Logger
{
    public abstract class LogBase : ILog
    {
        private string _serviceName;
        public LogBase(string serviceName)
        {
            _serviceName = serviceName;
        }

        public void Info(string message)
        {
            LogInternal($"{DateTime.Now.ToString()} - {_serviceName} -  INFO: {message}");
        }
        public void Warning(string message)
        {
            LogInternal($"{DateTime.Now.ToString()} - {_serviceName} -  WARN: {message}");
        }
        public void Error(string message)
        {
            LogInternal($"{DateTime.Now.ToString()} - {_serviceName} -  ERROR: {message}");
        }
        public abstract void LogInternal(string message);

    }
}