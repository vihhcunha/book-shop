using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Models;
using Book_Shop.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(BookShopContext context) : base(context) { }

        public async Task<Address> GetAddressByProvider(Guid providerId)
        {
            return await _context
                .Addresses
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(_ => _.ProviderId == providerId);
        }
    }
}
