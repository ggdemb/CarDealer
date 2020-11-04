using CarDealer.Application.Sale;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarDealer.Application
{
    public static class DependencyInjectionRegistrant
    {
        public static IServiceCollection RegisterApplicationDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
