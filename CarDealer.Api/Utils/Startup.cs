using CarDealer.Application;
using CarDealer.Persistence;
using CarDealer.Sale.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Utils
{
    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        services.AddMvc();

    //        services.AddSingleton(new SessionFactory(Configuration["ConnectionString"]));
    //        services.AddScoped<UnitOfWork>();
    //        services.AddTransient<MovieRepository>();
    //        services.AddTransient<CustomerRepository>();
    //    }

    //    public void Configure(IApplicationBuilder app)
    //    {
    //        app.UseMiddleware<ExceptionHandler>();
    //        app.UseMvc();
    //    }
    //}

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterPersistanceDependencyInjection(Configuration);
            services.RegisterApplicationDependencyInjection();
            services.RegisterDomainDependencyInjection();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
