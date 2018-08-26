using System; 
using System.Collections.Generic; 
using System.IO; 
using System.Linq; 
using System.Threading.Tasks; 
using Microsoft.AspNetCore; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.Logging; 
using Serilog; 

namespace localmarket {
    public class Program {
        public static void Main (string[] args) {
            Log.Logger = 
             new LoggerConfiguration ()
                .MinimumLevel.Debug ()
                .WriteTo.Console ()
                .WriteTo.Seq ("http://localhost:5341")
                .WriteTo.File ("log.clef", rollingInterval:RollingInterval.Day, outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger (); 

            Log.Information ("Local Market webhost is starting!"); 

            BuildWebHost (args).Run (); 

            Log.Information ("Local Market webhost is successfully started!"); 

            Log.CloseAndFlush (); 
        }

        public static IWebHost BuildWebHost (string[] args) => 
            WebHost.CreateDefaultBuilder (args)
            .UseKestrel ()
            .UseContentRoot (Directory.GetCurrentDirectory ())
            .ConfigureAppConfiguration ((builderContext, config) =>  {

                IHostingEnvironment env = builderContext.HostingEnvironment; 

                config.SetBasePath (Directory.GetCurrentDirectory ())
                    .AddJsonFile ("appsettings.json", optional:true, reloadOnChange:true)
                    .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional:true, reloadOnChange:true)
                    .AddEnvironmentVariables (); 

                if (args != null) {
                    config.AddCommandLine (args); 
                }
            })
            .UseIISIntegration ()
            .UseDefaultServiceProvider ((context, options) =>  {
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment (); 
            })
            .UseStartup < Startup > ()
            .Build (); 
    }
}