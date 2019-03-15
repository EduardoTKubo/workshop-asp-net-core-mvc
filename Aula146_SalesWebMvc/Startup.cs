using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;             // para o locale
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Aula146_SalesWebMvc.Models;
using Aula146_SalesWebMvc.Data;
using Aula146_SalesWebMvc.Services;


namespace Aula146_SalesWebMvc
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<Aula146_SalesWebMvcContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("Aula146_SalesWebMvcContext"), builder =>
                        builder.MigrationsAssembly("Aula146_SalesWebMvc")));

            // registra o servico SeedingService no sistema de injecao de depencia da aplicacao
            services.AddScoped<SeedingService>();
            services.AddScoped<SellerService>();
            services.AddScoped<DepartmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env ,SeedingService seedingService )
        {
            // definindo o locale da aplicacao como sendo dos EUA
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };
            app.UseRequestLocalization(localizationOptions);
            

            if (env.IsDevelopment())             // 1. se eu estiver no perfil de desenvolvimento ...
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();           // 1.. o app vai popular a base de dados se nao estiver populada
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
