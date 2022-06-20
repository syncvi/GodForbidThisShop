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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoppingAttire.DAL;
using ShoppingAttire.Data;
using ShoppingAttire.Middleware;

namespace ShoppingAttire
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

            services.AddAuthentication("CookieAuthentication")
               .AddCookie("CookieAuthentication", config =>
               {
                   config.Cookie.HttpOnly = true;
                   config.Cookie.SecurePolicy = CookieSecurePolicy.None;
                   config.Cookie.Name = "UserLoginCookie";
                   config.LoginPath = "/Login/UserLogin";
                   config.Cookie.SameSite = SameSiteMode.Strict;
               });
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin");
                options.Conventions.AuthorizeFolder("/CartUtils");
                options.Conventions.AuthorizePage("/MainFun/Insert");
                options.Conventions.AuthorizePage("/MainFun/Update");
                options.Conventions.AuthorizePage("/MainFun/Delete");
                options.Conventions.AuthorizePage("/CategoryFun/Insert");
                options.Conventions.AuthorizePage("/CategoryFun/Update");
                options.Conventions.AuthorizePage("/CategoryFun/Delete");
            });

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddRazorPages();
            services.AddDbContext<WorkingContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("WorkingDB")));//--------------------------------------IMPORTANT----------

            //services.Add(new ServiceDescriptor(typeof(IProductDB), new ProductXmlDB(Configuration)));
            services.Add(new ServiceDescriptor(typeof(IProductDB), new ProductSqlDB(Configuration)));
            services.AddTransient<ILogger, BigLogger>();

            /*services.AddDbContext<AnyContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AnyContext")));*/

            // services.Add(new ServiceDescriptor(typeof(IProductDB), new ProductYamlDB(Configuration)));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseImageMiddleware(); //easter egg
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<AuthorizeMiddleware>();

            

            app.UseHttpsRedirection();

            app.UseSession();

            app.UseStaticFiles();

            app.UseCookiePolicy(); //+

            app.UseAuthentication(); //+

            app.UseRouting();

            app.UseAuthorization(); //+

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
