using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_Assignment1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "Fever Check",
                    "fevercheck/{id?}",
                    new {controller = "default", action ="fevercheck"}
                    );
                routes.MapRoute(
                    "Guessing Game",
                    "guessinggame",
                    new { controller = "default", action = "guessinggame" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=default}/{action=index}/{id?}");
            });
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}
