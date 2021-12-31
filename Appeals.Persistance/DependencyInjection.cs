using Appeals.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appeals.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<AppealsDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped(provider =>
            {
                return GetService(provider);
            });
            return services;
        }

        private static IAppealsDbContext GetService(IServiceProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            var service = provider.GetService<AppealsDbContext>();
            if (service == null)
                throw new ArgumentNullException(nameof(provider));
            return service;
        }
    }
}
