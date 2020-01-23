using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Optimation.Service.ModuleRegistration;
using Optimation.WebApi.Middlewares;
using System;

namespace Optimation.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register API versioning
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // Register swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Optimation REST API", 
                    Version = "v1", 
                    Description = "Documentation of Optimation REST API" 
                });
            });

            return new AutofacServiceProvider(BuildAutofacContainer(services));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Optimation Rest API V1");
                // Serve swagger at the route
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            RegisterCustomMiddlewares(app);
            app.UseMvc();
        }

        /// <summary>
        /// Builds a DI container from all registered components
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <returns>Container Contract</returns>
        private IContainer BuildAutofacContainer(IServiceCollection services)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            RegisterAdditionalDependencies(builder);
            return builder.Build();
        }

        /// <summary>
        /// Register other required dependencies from other assemblies
        /// </summary>
        /// <param name="builder">Container builder</param>
        private void RegisterAdditionalDependencies(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules(typeof(ServiceModule).Assembly);
        }

        /// <summary>
        /// Registers custom middleware
        /// </summary>
        /// <param name="app"></param>
        private void RegisterCustomMiddlewares(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        }
    }
}
