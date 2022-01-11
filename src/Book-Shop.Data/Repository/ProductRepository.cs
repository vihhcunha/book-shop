using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Models;
using Book_Shop.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(BookShopContext context) : base(context) { }

        public async Task<Product> GetProductProvider(Guid id)
        {
            return await _context.Products
                .AsNoTrackingWithIdentityResolution()
                .Include(_ => _.Provider)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId)
        {
            return await Search(_ => _.ProviderId == providerId);
        }

        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await _context.Products
                .AsNoTrackingWithIdentityResolution()
                .Include(_ => _.Provider)
                .OrderBy(_ => _.Name)
                .ToListAsync();
        }
    }
}
