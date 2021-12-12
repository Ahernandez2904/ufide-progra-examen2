using Gadgets.Application;
using Gadgets.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gadgets.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterApplicationServices(Configuration);
            services.RegisterInfrastructureServices(Configuration);

            services.ConfigureApplicationCookie
                (
                    options =>
                    {
                        options.Cookie.HttpOnly = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                        options.SlidingExpiration = true;
                    }
                );

            services.AddControllersWithViews().AddJsonOptions
                (options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.Use
                (
                    async (context, next) =>
                    {
                        var token = context.Session.GetString("Token");
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Request.Headers.Add
                                (HeaderNames.Authorization, string.Concat("Bearer ", token));
                        }
                        await next();
                    }
                );

            app.UseEndpoints
                (
                    endpoints =>
                    {
                        endpoints.MapControllerRoute
                            (
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}"
                            );
                    }
                );
        }
    }
}
