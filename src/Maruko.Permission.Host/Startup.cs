using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Maruko.Configuration;
using Maruko.Permission.Core.ORM.Extension;
using Maruko.Permission.Host.Extension;
using Maruko.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Permission.Host
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                ConfigurationSetting.DefaultConfiguration = builder.Build();
            }
            else
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);//WebApi项目配置
                ConfigurationSetting.DefaultConfiguration = builder.Build();
            }
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddConfigureServices(service =>
            {
                services.AddMemoryCache().AddDeafultDbContext();
            });

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterModules();
            ContainerManager.Current = containerBuilder.Build();
            return new AutofacServiceProvider(ContainerManager.Current);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAspNetMvcConfigure(env, application =>
            {
                application.UseAuthentication().UseMvcWithDefaultRoute().UseCors("cors");
            });
        }
    }
}
