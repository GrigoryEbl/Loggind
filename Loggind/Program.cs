using System;
using System.IO;

namespace Lesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder consoleLogWriter = new Pathfinder(new ConsoleLogWriter());
            Pathfinder fileLogWriter = new Pathfinder(new FileLogWriter());
            Pathfinder secureConsoleLogWriter = new Pathfinder(new ConsoleLogWriter());
            Pathfinder secureFileLogWriter = new Pathfinder(new SecureFileLogWriter());
            Pathfinder secureConsoleAndFileLogWriter = new Pathfinder(new SecureConsoleAndFileLogWriter());

            consoleLogWriter.Find("console log");
            fileLogWriter.Find("file log");
            secureConsoleLogWriter.Find("secure Console Log ");
            secureFileLogWriter.Find("secure file log ");
            secureConsoleAndFileLogWriter.Find("secure Console And File Log ");
        }
    }

    interface ILogger
    {
        void Find(string message);
    }

    class Pathfinder : ILogger
    {
        private ILogger _logger;

        public Pathfinder(ILogger logger)
        { 
            _logger = logger;
        }

        public void Find(string message)
        {
            _logger.Find(message);
        }
    }

    class ConsoleLogWriter : ILogger 
    {
        public void Find(string message)
        {
           WriteError(message);
        }

        public virtual void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWriter : ILogger
    {
        public void Find(string message)
        {
            WriteError(message);
        }

        public virtual void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureConsoleLogWriter : ConsoleLogWriter
    {
        public override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                base.WriteError(message);
            }
        }
    }

    class SecureFileLogWriter : FileLogWriter
    {
        public override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                base.WriteError(message);
            }
        }
    }

    class SecureConsoleAndFileLogWriter : ConsoleLogWriter
    {
        public override void WriteError(string message)
        {
            base.WriteError(message);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                File.WriteAllText("log.txt", message);
            }
        }
    }
}