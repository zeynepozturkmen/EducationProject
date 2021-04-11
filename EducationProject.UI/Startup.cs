using EducationProject.Data.DbContexts;
using EducationProject.UI.IoC;
using FluentValidation.AspNetCore;
using FormHelper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EducationProject.UI
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
            ServiceInjector.Add(services, Configuration);
            ValidationService.Add(services);

            services.AddSession();
            services.AddHttpContextAccessor();



            //sayfayi yenileyince yenilikleri algilamasi icin
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddControllersWithViews()
.AddFluentValidation();

            services.AddFormHelper(new FormHelperConfiguration
            {
                CheckTheFormFieldsMessage = "Check the form fields."
                //RedirectDelay->Y�nlendirme i�lemlerinde beklenecek varsay�lan s�re.
                //ToastrDefaultPosition->Bildirim / Uyar� mesajlar�n�n ekranda g�r�nece�i pozisyon.
            });

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


            app.UseFormHelper();

            // uygulaman�n varsay�lan dilini T�rk�e olarak �al��maya zorlamak icin
            var cultureInfo = new CultureInfo("tr-TR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });

            EducationDbSeed.Initialize(app.ApplicationServices, true);

        }
    }
}
