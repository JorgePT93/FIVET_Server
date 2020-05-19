using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fivet.Server
{
    public class Program
    {
    /// <summary>
    /// Main Starting Point
    /// <summary>
    /// <param name ="args"></param>
    public static void Main (string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Build And Configure A Host
    /// <summary>
    /// <returns>The IHostBuilder</returns>
    public static IHostBuilder CreateHostBuilder(string[] args)=>
        Host
        .CreateDefaultBuilder(args)
        // Development, Staging, Production
        .UseEnvironment("Development")
        // Logging Configuration
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole(options =>
            {
                options.IncludeScopes = true;
                options.TimestampFormat = "[yyyyMMdd.HHmmss.fff] ";
                options.DisableColors = false;
            });
            logging.SetMinimumLevel(LogLevel.Trace);
        })
        // Enable Control+C listener
        .UseConsoleLifeTime()
        // Service Inside The DI
        .ConfigureServices((context, services) =>
        {
            // The FivetService
            services.AddHostedService<FivetService>();
            // The logger
            services.AddLogging();
            // The Wait 4 Finish
            services.Configure<HostOptions>(option =>
            {
                option.ShutdownTimeout = System.TimeSpan.FromSeconds(15);
            });
        });    
    }
}
