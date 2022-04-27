using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SerapisMedicalAPI.Helpers;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace SerapisMedicalAPI
{
    public class Program
    {
        public static int Main(string[] args)
        {

            //https://www.youtube.com/watch?v=_iryZxv8Rxw
            Log.Logger = new LoggerConfiguration()
            //.ReadFrom.Configuration(configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.With(new ThreadIdEnricher())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/logs.txt", restrictedToMinimumLevel: LogEventLevel.Information,
                                            rollingInterval: RollingInterval.Hour,
                                            outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
            //.WriteTo.File("logs/Errorlogs.txt", restrictedToMinimumLevel: LogEventLevel.Warning,
            //                                    rollingInterval: RollingInterval.Hour)
            //.WriteTo.File(new CompactJsonFormatter(), "logs/Jsonlogs.json")
            .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    //webBuilder.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));
                });
        //.UseStartup<Startup>();
    }
}
