using EducationProject.Core.Entities;
using EducationProject.Data.DbContexts;
using EducationProject.Service.IService;
using EducationProject.Service.IUnitOfWorks;
using EducationProject.Service.Service;
using EducationProject.Service.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationProject.UI.IoC
{
    public class ServiceInjector
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");

            services.AddDbContext<EducationDbContext>(options => options.UseSqlServer(connectionString, optionsBuilder =>
                optionsBuilder.MigrationsAssembly("EducationProject.Data")), ServiceLifetime.Scoped
            );

            services.AddIdentity<User, Role>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequiredLength = 5; //En az kaç karakterli olması gerektiğini belirtiyoruz.
                cfg.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunluluğunu kaldırıyoruz.
                cfg.Password.RequireLowercase = false; //Küçük harf zorunluluğunu kaldırıyoruz.
                cfg.Password.RequireUppercase = false; //Büyük harf zorunluluğunu kaldırıyoruz.
                cfg.Password.RequireDigit = false; //0-9 arası sayısal karakter zorunluluğunu kaldırıyoruz.
                cfg.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<EducationDbContext>().AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(options =>
            {
                //Javascript document diyerek cooki'deki(giriş yapan kullanıcının bilgileri vs) ulasilmasını engelliyor.
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "Test";
                //Sub domain'de dahil olmak üzere diğer sitelerin cookie'ye erişimini kapatıyor
                options.Cookie.SameSite = SameSiteMode.Strict;
                //Http ya da https üzerinden çalışıyor
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                //Yetki yoksa;yetkiniz yok sayfası dönüyor
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                //options.ExpireTimeSpan = TimeSpan.FromDays(14);
                options.SlidingExpiration = true;
            });



            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
    

        }
    }
}
