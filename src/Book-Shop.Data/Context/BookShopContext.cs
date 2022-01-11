using Book_Shop.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Data.Context
{
    public class BookShopContext : DbContext
    {
        public BookShopContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookShopContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
