using Database.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ÊóðñîâàÿBusinessLogic.Interfaces;

namespace WebClient
{

        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllersWithViews();
                services.AddTransient<IClientLogic, ClientLogic>();
                services.AddTransient<IUserLogic, UserLogic>();
       //         services.AddTransient<IAgentLogic, AgentLogic>();
            //    services.AddTransient<IVisitLogic, VisitLogic>();
            //    services.AddTransient<IDoctorLogic, DoctorLogic>();
            //     services.AddTransient<BackUpAbstractLogic, BackUpLogic>();
            //     services.AddTransient<ReportLogic>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }
                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            }
        }
    }