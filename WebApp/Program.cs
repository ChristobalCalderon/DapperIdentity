using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using WebApp.Infrastructure.SqlDatabase.Migration;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            var runMigration = args.Any(x => x == "/migrate");
            if (runMigration) args = args.Except(new[] { "/migrate" }).ToArray();

            if(runMigration)
            {
                //TODO GET connectionstring from Environment
                MigrationService.Migrate(@"Data Source=localhost\\SQLEXPRESS01;Initial Catalog=teamleites;Integrated Security=True");
                return;
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File("dapperidentity_logs.txt")
                .CreateLogger();

            return WebHost.CreateDefaultBuilder(args)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var env = hostingContext.HostingEnvironment;
                            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                                optional: true, reloadOnChange: true);
                            config.AddEnvironmentVariables();
                        }).ConfigureLogging(builder =>
                        {
                            builder.ClearProviders();
                            builder.AddSerilog();
                        })
                 .UseStartup<Startup>()
                 .Build();
        }

    }
}
