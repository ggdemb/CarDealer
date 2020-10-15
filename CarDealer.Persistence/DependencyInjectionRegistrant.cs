using CarDealer.Application.CommonContracts;
using CarDealer.Application.Sale;
using CarDealer.Persistence.Repositories.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarDealer.Persistence
{
    public static class DependencyInjectionRegistrant
    {
        public static IServiceCollection RegisterPersistanceDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarDealerContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(CarDealerContext).Assembly.FullName)));
            services.AddScoped<ICarDealerContext>(provider => provider.GetService<CarDealerContext>());
            services.AddScoped<ICarRepository>(provider => provider.GetService<CarRepository>());
            services.AddScoped<IUnitOfWork>(provider => provider.GetService<UnitOfWork>());

            return services;
        }
    }
}
