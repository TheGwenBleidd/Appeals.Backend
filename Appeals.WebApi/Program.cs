using Appeals.Persistance;

namespace Appeals.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) 
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<AppealsDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var title = "kdts";

                        // TODO: publish
                        var environment = "local";
                        // var environment = "demo";
                        // var environment = "development";
                        // var environment = "production";
                        config.AddJsonFile($"Configs/{title}.{environment}.json", false, false);
                    })
                    .UseStartup<Startup>()
                    .UseDefaultServiceProvider(options => options.ValidateScopes = false);
                });
    }
}
