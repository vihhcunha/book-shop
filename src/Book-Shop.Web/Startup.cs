using Book_Shop.Business.Interfaces;
using Book_Shop.Data.Context;
using Book_Shop.Data.Repository;
using Book_Shop.Web.Areas.Identity.Data;
using Book_Shop.Web.Configurations;
using Book_Shop.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);

            services.AddDbContext<BookShopContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.ResolveDependencies();

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapRazorPages();
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                routes.MapControllers();
            });
        }
    }
}
