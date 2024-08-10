using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Tests
{
    public class Logger
    {
        private readonly ILogger _logger;
        public ILogger Instance { get => _logger; }
        public Logger() 
        {
            _logger = new LoggerConfiguration().WriteTo.File("logs.txt").WriteTo.Console().CreateLogger();
        }
    }
}
