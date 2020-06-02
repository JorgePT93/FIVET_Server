using Fivet.Dao;
using Fivet.ZeroIce;
using Fivet.ZeroIce.model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Fivet.Server
{
    public class Program
    {
        /// <summary>
        /// Main starting point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Build and configure a Host
        /// </summary>
        /// <param name="args"></param>
        /// <returns>The IhostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            return Host
            .CreateDefaultBuilder(args)
            // Development, Staging, Production
            .UseEnvironment("Development")
            // Logging configuration
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss.fff]";
                    options.DisableColors = false;
                });
                logging.SetMinimumLevel(LogLevel.Trace);
            })
            // Enable Control+C listener
            .UseConsoleLifetime()
            // Service inside the DI
            .ConfigureServices((context, services) =>
            {
                // TheSystem
                services.AddSingleton<TheSystemDisp_, TheSystemImpl>();
                // Contratos
                services.AddSingleton<ContratosDisp_, ContratosImpl>();
                // The FivetContext
                services.AddDbContext<FivetContext>();
                // The FivetService
                services.AddHostedService<FivetService>();
                // The logger
                services.AddLogging();
                // The wait 4 finish
                services.Configure<HostOptions>(option =>
                {
                    option.ShutdownTimeout = System.TimeSpan.FromSeconds(15);
                });
            });
        }
    }
}
