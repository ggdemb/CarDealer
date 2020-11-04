using Api.Utils;
using CarDealer.Api.Common;
using CarDealer.Application.CommonContracts;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace CarDealer.Application
{
    public static class DependencyInjectionRegistrant
    {
        public static IServiceCollection RegisterApiDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLogger<>));
            return services;
        }
    }
}
