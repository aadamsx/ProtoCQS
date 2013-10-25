using System;

namespace ProtoConsole
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class DatabaseLogger : ILogger
    {
        public void Log(string message)
        {
            // database logging
        }
    }

    public class Logger
    {
        private readonly ILogger _logger;

        public Logger(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.Log(message);
        }
    }
}