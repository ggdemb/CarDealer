using CarDealer.Domain.Common;
using CarDealer.Domain.SharedKernel;
using Microsoft.Extensions.DependencyInjection;

namespace CarDealer.Sale.Domain
{
    public static class DependencyInjectionRegistrant
    {
        public static IServiceCollection RegisterDomainDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IDateTime, DateTimeService>();
            return services;
        }
    }
}
