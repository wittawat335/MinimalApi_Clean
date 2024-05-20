using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ApplicationLayer
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            _ = services.AddMediatR(Assembly.GetExecutingAssembly());
            _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //_ = services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }

}
