using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogging
{
    class Program
    {
        static void Main(string[] args)
        {
        }
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

    public interface ILogger
    {
        void Log(string message);
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
        private readonly log4net.ILog log;

        public DatabaseLogger(log4net.ILog log4Net)
        {
            log = log4Net;
        }

        public void Log(string message)
        {
            // Code to send message to table
        }
    }
}
