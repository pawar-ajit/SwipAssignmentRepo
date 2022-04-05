using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserSwipeAssignment.Utilities
{
    public class Logger
    {
        private static readonly Logger _instance = new Logger();
        protected ILog monitoringLogger;
        protected static ILog debugLogger;

        private Logger()
        {
            monitoringLogger = LogManager.GetLogger("MonitoringLogger");
            debugLogger = LogManager.GetLogger("DebugLogger");
        }

        public static void Debug(string message)
        {
            debugLogger.Debug(message);
        }

        public static void Error(string message, System.Exception exception)
        {
            _instance.monitoringLogger.Error(message, exception);
        }

    }
}