using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CustomerMaster.Models;

namespace CustomerMaster
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // services.AddRazorPages(); // Uncomment if you are using Razor Pages
            // Add other services here (e.g., DbContext, etc.)

            // Register CustomerMasterContext for Dependency Injection
            services.AddDbContext<CustomerMasterContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("OutsideSvcPOsContext")));
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireCMShippingCommentsRole", policy =>
                      policy.RequireRole("CMShippingCommentsRole")); // Define the role requirement here
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting(); // **Endpoint Routing Middleware**

            app.UseAuthorization(); // **Authorization Middleware (if needed)**

            app.UseEndpoints(endpoints => // **Endpoint Routing Configuration**
            {
                endpoints.MapControllerRoute( // Maps controller routes (for MVC controllers)
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // endpoints.MapRazorPages(); // Uncomment if you are using Razor Pages
            });
        }
    }
}