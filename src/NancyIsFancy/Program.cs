using System;
using Nancy.Hosting.Self;
using Topshelf;

namespace NancyIsFancy
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9876";

            HostFactory.Run(x =>                                 
            {
                x.Service<NancyHost>(s =>                       
                {
                    s.ConstructUsing(name => new NancyHost(new Uri(url)));     
                    s.WhenStarted(host =>
                        {
                            Console.WriteLine("Nancy configured to listen on " + url);
                            host.Start();
                        });              
                    s.WhenStopped(host =>
                        {
                            host.Stop();
                            host.Dispose();
                        });               
                });

                x.RunAsLocalSystem();                            

                x.SetDescription("Nancy Is Fancy");
                x.SetDisplayName("NancyIsFancy");
                x.SetServiceName("NancyIsFancy");                       
            }); 
        }
    }
}
