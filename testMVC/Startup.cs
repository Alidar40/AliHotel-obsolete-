using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;
using testMVC.Dtos;
using Newtonsoft.Json;


namespace testMVC
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

            // добавление сервисов Idenity
            /*services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();*/

            // добавление ApplicationDbContext для взаимодействия с базой данных учетных записей
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));
            services.AddMvc()
            .AddJsonOptions(options => {
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             });
            //camel notation:
            /*.AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver
                     = new Newtonsoft.Json.Serialization.DefaultContractResolver();
             });
            */


            //services.AddApiVersioning();

            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                /*routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");*/
            });
            //app.UseApiVersioning();
        }
    }
}
