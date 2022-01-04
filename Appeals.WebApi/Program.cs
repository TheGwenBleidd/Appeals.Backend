using Appeals.Persistance;
using Serilog;
using Serilog.Events;

namespace Appeals.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File("AppealsWebAppLog-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
                ;
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) 
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<AppealsDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "An error occured while app started");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }
                );
    }
}
