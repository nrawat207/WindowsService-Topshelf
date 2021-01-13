using log4net;
using System;

namespace WindowsServiceTopshelf
{
    public class Service : IService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Service));

        public void Execute()
        {
            log.DebugFormat("Service execution started");

            Console.WriteLine("Service Executed successfully!");
        }
    }
}
