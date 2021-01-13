using log4net;
using System;
using System.Configuration;

namespace WindowsServiceTopshelf
{
    public class ServiceSettings
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ServiceSettings));

        public static double TimerInterval
        {
            get
            {
                var timerInterval = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInterval"]);

                if (timerInterval > 1000) return timerInterval;

                log.Warn("To low interval. Will multiply with 1000");
                timerInterval *= 1000;

                return timerInterval;
            }
        }
    }
}
