using CarDealer.Api.Common;
using CarDealer.Application.CommonContracts;
using CarDealer.Application.Sale;
using Microsoft.Extensions.DependencyInjection;

namespace CarDealer.Application
{
    public static class DependencyInjectionRegistrant
    {
        public static IServiceCollection RegisterApiDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
