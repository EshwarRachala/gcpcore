using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace localmarket {
    public class Program {
        public static void Main (string[] args) {

            BuildWebHost (args).Run ();
        }

        public static IWebHost BuildWebHost (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseKestrel ()
            .UseContentRoot (Directory.GetCurrentDirectory ())
            .ConfigureAppConfiguration ((builderContext, config) => {

                IHostingEnvironment env = builderContext.HostingEnvironment;

                config.SetBasePath (Directory.GetCurrentDirectory ())
                    .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
                    .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true, reloadOnChange : true)
                    .AddEnvironmentVariables ();

                if (args != null) {
                    config.AddCommandLine (args);
                }
            })
            .ConfigureLogging ((hostingContext, logging) => {
                logging.AddConfiguration (hostingContext.Configuration.GetSection ("Logging"));
                logging.AddConsole ();
                logging.AddDebug ();
            })
            .UseIISIntegration ()
            .UseDefaultServiceProvider ((context, options) => {
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment ();
            })
            .UseStartup<Startup> ()
            .Build ();
    }
}