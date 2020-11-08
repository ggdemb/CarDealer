using CarDealer.Application.ExternalContracts;
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
            services.AddScoped<ICarDealerContext, CarDealerContext>();
            //services.AddScoped<IUnitOfWorkContext>(provider => provider.GetService<CarDealerContext>()); add same context 
            // object as diffrent interface - you need this in UnitOfWork implementation. You have to sure it use the same instance.
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
