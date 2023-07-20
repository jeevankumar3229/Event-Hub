
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplicationProject
{
    public static class LoggerConfig
    {
        private static ILogger logger;

        public static void Initialize()
        {
            logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(@"C:\Users\286968\OneDrive - Resideo\Desktop\POC\DevicesAPI\WindowsApplicationProject\Logs\loggingdetails123.txt", rollingInterval:RollingInterval.Day).CreateLogger();
        }

        public static void _LogInformation(string message)
        {
            logger.Information(message);
        }

        public static void _LogError(string message,Exception ex)
        {
            logger.Error(ex, message);
        }
    }
}
