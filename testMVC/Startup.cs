using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using testMVC.Models;
using testMVC.Dtos;
using testMVC.Data;
using testMVC.Services;
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
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // добавление ApplicationDbContext для взаимодействия с базой данных учетных записей
            //string connection = Configuration.GetConnectionString("DefaultConnection");
            
			services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
			// Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            /*var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            //.RequireRole("Admin", "SuperUser")
            .Build();*/



            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            
            /*.AddJsonOptions(options => {
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             });*/


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
                app.UseDatabaseErrorPage();
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
