using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using diary.Data;
using diary.Models;
using diary.Services;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace diary
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
            string localhostOrigin = "";
            if (diary.Program.WEB_PORT == "80")
            {
                localhostOrigin = "http://localhost";
            }
            else
            {
                localhostOrigin = "http://localhost:" + diary.Program.WEB_PORT;
            }            

            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhostOrigin",
                    builder => builder.WithOrigins(localhostOrigin)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc()
                    .AddJsonOptions(options => 
                    {
                        options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
                    });;

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            //Add session
            services.AddSession(options =>
            {
                options.Cookie.Name = ".Diary.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            app.UseSession();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCors("AllowLocalhostOrigin");

            app.Use(async (context, next) =>
            {
                int? port = context.Request.Host.Port;
                if (port.HasValue && port.Value == 8080 && !context.Request.Path.Value.StartsWith("/api"))
                {
                    ProxyHandler handler = new ProxyHandler(context);
                    handler.ProxyToApi();
                    handler.WriteResponse(context.Response);
                }
                else 
                {
                    //continues through the rest of the pipeline
                    await next();
                }
            });

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                
            });
        }
    }
}
