using Book_Shop.Business.Models;

namespace Book_Shop.Business.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderAddress(Guid id);
        Task<Provider> GetProviderProductsAddress(Guid id);
    }
}
