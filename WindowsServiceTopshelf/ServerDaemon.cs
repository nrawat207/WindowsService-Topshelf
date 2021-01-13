using log4net;
using System;
using System.Collections.Generic;
using System.Timers;

namespace WindowsServiceTopshelf
{
    public class ServerDaemon
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ServerDaemon));
        private readonly List<IService> services;
        private readonly Timer timer = new Timer();

        public ServerDaemon(List<IService> services)
        {
            if (services == null) throw new ArgumentNullException("services");            
            this.services = services;
        }

        public void Start()
        {
            timer.Interval = ServiceSettings.TimerInterval;
            log.InfoFormat("Timer interval set to: {0} seconds", timer.Interval / 1000);
            timer.Start();
            timer.Elapsed += OnTimerElapsed;
            log.Info("Timer started");
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            services.ForEach(DoExecute);
            timer.Enabled = true;
        }

        private static void DoExecute(IService service)
        {
            try
            {
                service.Execute();
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Failed to execute: {0}", service.GetType().Name.Replace("Service", ""));
                log.Error("Exception: ", ex);
            }
        }

        public void Stop()
        {
            timer.Stop();
            log.Info("Timer stopped");
        }
    }
}
