using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLoggingNLog
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }


    public interface ILogger
    {
        void Log(string message);
    }

    public class Logger
    {
        private ILogger _logger;

        public Logger(ILogger logger)
        {
            this._logger = logger;
        }

        public void Log(string message)
        {
            _logger.Log(message);
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class PrinterLogger : ILogger
    {
        public void Log(string message)
        {
            // Code to send message to printer
        }
    }

    public class EmailLogger : ILogger
    {
        public void Log(string message)
        {
            // Code to send message to email
        }
    }

    public class DatabaseLogger : ILogger
    {
        private NLog.Logger log;

        public DatabaseLogger(NLog.Logger log)
        {
            this.log = log;
        }

        public void Log(string message)
        {
            // Code to send message to table
        }
    }
}
