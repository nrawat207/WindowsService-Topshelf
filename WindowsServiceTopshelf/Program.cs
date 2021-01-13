using log4net.Config;
using StructureMap;
using System.Linq;
using Topshelf;

namespace WindowsServiceTopshelf
{
    public class Program
    {
        public static void Main()
        {
            XmlConfigurator.Configure();

            var container = new Container(x =>
            {               
                x.For<IService>().Use<Service>();
            });
            var services = container.GetAllInstances<IService>().ToList();

            HostFactory.Run(x =>
            {

                x.Service<ServerDaemon>(s =>
                {
                    s.ConstructUsing(name => new ServerDaemon(services));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("WindowsServiceTopshelf");
                x.SetDisplayName("WindowsServiceTopshelf");
                x.SetServiceName("WindowsServiceTopshelf");
            });

        }
    }
}
