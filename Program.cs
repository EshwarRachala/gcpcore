using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
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
            .UseStartup<Startup> ()
            .UseContentRoot (Directory.GetCurrentDirectory ())
            .ConfigureAppConfiguration ((builderContext, config) => {
                config.AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true);
                // .AddJsonFile($"appsettings.{ env.EnvironmentName}.json", optional: true);
            })
            .Build ();
    }
}