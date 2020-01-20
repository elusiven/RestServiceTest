using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Optimation.Service.ModuleRegistration;
using System;

namespace Optimation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private IContainer BuildAutofacContainer(IServiceCollection services)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            RegisterAdditionalDependencies(builder);
            return builder.Build();
        }

        private void RegisterAdditionalDependencies(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules(typeof(ServiceModule).Assembly);
        }
    }
}
