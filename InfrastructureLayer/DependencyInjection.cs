using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                   ?? throw new InvalidOperationException("Connection string not found"),
                   sqlServerOptionsAction: options =>
                   {
                       options.EnableRetryOnFailure(
                       maxRetryCount: 10,
                       maxRetryDelay: TimeSpan.FromSeconds(5),
                       errorNumbersToAdd: null);
                   });
            });

            return services;
        }
    }
}
