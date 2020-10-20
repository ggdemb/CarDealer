using CarDealer.Application.CommonContracts;
using CarDealer.Infrastructure.Sale;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarDealer.Infrastructure
{
    public static class DependencyInjectionRegistrant
    {
        public static IServiceCollection RegisterInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IExternalSystemClient, ExternalSystemClient>(); //todo: use configuration to set endpoint url;

            return services;
        }
    }
}
