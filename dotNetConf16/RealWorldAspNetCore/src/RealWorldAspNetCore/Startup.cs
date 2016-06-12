using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace RealWorldAspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // yay, environment-based json configuration!
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog(new LoggerConfiguration()
                                        .MinimumLevel.Debug()
                                        .WriteTo.Seq("http://localhost:5000")
                                        .CreateLogger());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // the zero is matched with whatever status code there is.
            //app.UseStatusCodePagesWithRedirects("/{0}.html");

            // general mvc routing syntax is supported.
            app.UseStatusCodePagesWithReExecute("/StatusCode/");

            app.Use((context, next) =>
            {
                // this guard is needed because of the 'use status code pages with reexecute'
                // being enabled. Otherwise, we'd be stuck with 418 status code, because
                // re-execute means that this whole pipeline is re-executed completely.
                if (context.Response.StatusCode != 418)
                {
                    context.Response.StatusCode = 418;
                    return Task.FromResult(0);
                }
                else
                {
                    return next.Invoke();
                }
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
