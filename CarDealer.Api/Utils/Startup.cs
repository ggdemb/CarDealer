using CarDealer.Application;
using CarDealer.Application.CommonContracts;
using CarDealer.Domain.Common;
using CarDealer.Infrastructure;
using CarDealer.Persistence;
using CarDealer.Sale.Domain;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Api.Utils
{
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
            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
             {
                 //options.SuppressConsumesConstraintForFormFileParameters = true;
                 //options.SuppressInferBindingSourcesForParameters = true;
                 //options.SuppressModelStateInvalidFilter = true;
                 options.InvalidModelStateResponseFactory = (actionContext) =>
                 {
                     var modelValidationErrors = actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                     return new BadRequestObjectResult(Envelope.Error(modelValidationErrors));
                 };
                 //options.SuppressMapClientErrors = true;
                 //options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                 //    "https://httpstatuses.com/404";
             });
            services.RegisterPersistanceDependencyInjection(Configuration);
            services.RegisterApplicationDependencyInjection();
            services.RegisterDomainDependencyInjection();
            services.RegisterInfrastructureDependencyInjection(Configuration);
            services.RegisterApiDependencyInjection();
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IAntiforgery antiforgery)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            if (Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseMiddleware<ExceptionHandler>();
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //CSRF protection: https://blog.gerardbeckerleg.com/posts/xsrf-with-angular-and-dot-net-core-web-api/
            app.Use(next => context =>
            {
                string path = context.Request.Path.Value;

                if (
                    string.Equals(path, "/", StringComparison.OrdinalIgnoreCase))
                {
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
                }
                return next(context);
            });
        }
    }
}
