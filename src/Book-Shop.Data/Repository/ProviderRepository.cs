using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Models;
using Book_Shop.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Data.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(BookShopContext context) : base(context) { }

        public async Task<Provider> GetProviderAddress(Guid id)
        {
            return await _context.Providers
                .AsNoTrackingWithIdentityResolution()
                .Include(_ => _.Address)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Provider> GetProviderProductsAddress(Guid id)
        {
            return await _context.Providers
                .AsNoTrackingWithIdentityResolution()
                .Include(_ => _.Products)
                .Include(_ => _.Address)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }
    }
}
