using Book_Shop.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Web.Configurations
{
    public static class DatabaseConfig
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AspNetIdentityContextConnection");
            services.AddDbContext<AspNetIdentityContext>(opt => opt.UseSqlServer(connectionString));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddEntityFrameworkStores<AspNetIdentityContext>();

            return services;
        }
    }
}
