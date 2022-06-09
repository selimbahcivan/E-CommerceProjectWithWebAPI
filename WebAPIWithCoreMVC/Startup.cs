using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebAPIWithCoreMVC.ApiServices;
using WebAPIWithCoreMVC.ApiServices.Interfaces;
using WebAPIWithCoreMVC.Handler;

namespace WebAPIWithCoreMVC
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
            services.AddControllersWithViews();
            services.AddHttpContextAccessor(); //ApiService AuthContr. conf. sonrasý eklendi
            services.AddSession(); //ApiService AuthContr. conf. sonrasý eklendi
            services.AddScoped<AuthTokenHandler>();

            #region HttpClient
            services.AddHttpClient<IAuthApiService, AuthApiService>(opt =>
                {
                    opt.BaseAddress = new Uri("http://localhost:63510/api/");
                });
            services.AddHttpClient<IUserApiService, UserApiService>(opt =>
            {
                opt.BaseAddress = new Uri("http://localhost:63510/api/");
            }).AddHttpMessageHandler<AuthTokenHandler>(); // AuthTokenHandler.cs ve Exception'dan sonra eklendi sadece User için eklendi.
            #endregion

            #region Cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
                {
                    opt.LoginPath = "/Admin/Auth/Login";
                    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                    opt.SlidingExpiration = true;
                    opt.Cookie.Name = "mvccookie";
                });
            #endregion

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
            }

            app.UseSession(); //ApiService AuthContr. conf. sonrasý eklendi

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Cookie'den sonra eklendi.

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // ScaffoldingReadMe'den kopyalandý.
                //Admin/Home/Index
                endpoints.MapAreaControllerRoute(
                areaName: "Admin",
                name: "Admin",
                pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                //Home/Index
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}