﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataLayer.EfCode;
using BizDbAccess.GenericInterfaces;
using Microsoft.AspNetCore.Routing;
using BizData.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using REYMAN.Policies;
using Microsoft.AspNetCore.Authorization;

namespace REYMAN
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Usuario, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequiredLength = 4;
            })
            .AddEntityFrameworkStores<EfCoreContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };

                });

            Dictionary<string, int> levels = new Dictionary<string, int>
                {
                   { "common", 1 },
                   { "edit",  2  },
                   { "admin", 3  }
                };

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy(
                    "LevelTwoAuth",
                    policyBuilder => policyBuilder.AddRequirements(
                        new LevelAuthRequirement(levels, "Permission", 2)
                        ));

                cfg.AddPolicy(
                    "LevelThreeAuth",
                    policyBuilder => policyBuilder.AddRequirements(
                        new LevelAuthRequirement(levels, "Permission", 3)
                        ));
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<EfCoreContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection,
                b => b.MigrationsAssembly("DataLayer")));

            services.AddScoped<IUnitOfWork, EfCoreContext>();

            services.AddTransient<EfSeeder>();

            services.AddSingleton<IAuthorizationHandler, LevelHandler>();

            services.AddMvc(opt =>
            {
                if (Env.IsProduction())
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                //Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<EfSeeder>();
                    seeder.Seed();
                }
            }

            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Account}/{action=Login}");
        }
    }
}
