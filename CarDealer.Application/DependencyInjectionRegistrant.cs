using CarDealer.Application.Sale;
using Microsoft.Extensions.DependencyInjection;

namespace CarDealer.Application
{
    public static class DependencyInjectionRegistrant
    {
        public static IServiceCollection RegisterApplicationDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();

            return services;
        }
    }
}
