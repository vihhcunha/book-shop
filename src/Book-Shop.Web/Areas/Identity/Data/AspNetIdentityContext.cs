using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Web.Areas.Identity.Data
{
    public class AspNetIdentityContext : IdentityDbContext<IdentityUser>
    {
        public AspNetIdentityContext(DbContextOptions<AspNetIdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
