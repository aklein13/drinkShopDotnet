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
using dotnetDrinks.Data;
using dotnetDrinks.Models;
using dotnetDrinks.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace dotnetDrinks
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProjektContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var skipSSL = Configuration.GetValue<bool>("LocalTest:skipSSL");
            if (env.IsDevelopment() && !skipSSL)
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
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
            });

            async Task func()
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new IdentityRole("Admin");
                    await roleManager.CreateAsync(role);

                    const string userName = "admin@admin.com";
                    const string userPass = "12345";

                    var user = new ApplicationUser { UserName = userName, Email = userName };
                    userManager.CreateAsync(user, userPass).Wait();
                    userManager.AddToRoleAsync(user, "Admin").Wait();

                }
                if (!await roleManager.RoleExistsAsync("Mod"))
                {
                    var role = new IdentityRole("Mod");
                    await roleManager.CreateAsync(role);

                    const string userName = "mod@mod.com";
                    const string userPass = "12345";

                    var user = new ApplicationUser { UserName = userName, Email = userName };
                    userManager.CreateAsync(user, userPass).Wait();
                    userManager.AddToRoleAsync(user, "Mod").Wait();

                }
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    var role = new IdentityRole("User");
                    await roleManager.CreateAsync(role);

                    const string userName = "user@user.com";
                    const string userPass = "12345";

                    var user = new ApplicationUser { UserName = userName, Email = userName };
                    userManager.CreateAsync(user, userPass).Wait();
                    userManager.AddToRoleAsync(user, "User").Wait();

                }

            }
            Task task = func();
            task.Wait();
        }

    }
}