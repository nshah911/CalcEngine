using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace FileManagerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //logger.Debug("init main");
                BuildWebHost(args).Run();
            }
            catch
            {
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>()
                .ConfigureLogging((host, builder) =>
                {
                    if (host.HostingEnvironment.IsDevelopment())
                        host.HostingEnvironment.ConfigureNLog("nlog.config");
                    else
                        host.HostingEnvironment.ConfigureNLog("nlog." + host.HostingEnvironment.EnvironmentName + ".config");

                    builder.ClearProviders();
                    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog()  // NLog: setup NLog for Dependency injection
                .Build();
    }
}
