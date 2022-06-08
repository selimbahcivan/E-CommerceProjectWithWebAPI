using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Mappings;
using Core.Extensions;
using Core.Utilities.Security.Token;
using Core.Utilities.Security.Token.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace WebAPI
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
            IServiceCollection servicesCollections = services.AddDbContext<ECommerceProjectWithWebAPIContext>(opt => opt.UseSqlServer("Server=.;" +
                "Database=eCommerceProject;Trusted_Connection=True;"
                , options => options.MigrationsAssembly("DataAccess")
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "dbo")));

            services.AddControllers();

            services.AddCustomSwagger(); // SwaggerExtensions.cs 'den sonra eklendi.
            services.AddCustomJwtToken(Configuration); // JwtTokenExt. sonra eklendi.

            #region AutoMapper

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion AutoMapper

            #region Dependency Injection

            services.AddTransient<IUserDal, EfUserDal>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddTransient<IAuthService, AuthService>();

            #endregion Dependency Injection
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCustomSwagger(); // SwaggerExtensions.cs 'den sonra eklendi.

                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthentication(); // first this
            app.UseAuthorization(); // second.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}