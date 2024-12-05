using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiManagementSystem.Logger
{
    public class ConsoleLogger : LogBase
    {
        public ConsoleLogger(string serviceName)
            : base(serviceName)
        {

        }
        public override void LogInternal(string message)
        {
            Console.WriteLine(message);
        }
    }
}