using Book_Shop.Business.Models;

namespace Book_Shop.Business.Interfaces.Services
{
    public interface IProviderService : IDisposable
    {
        Task Add(Provider provider);
        Task Update(Provider provider);
        Task Remove(Guid id);
        Task UpdateAddress(Address address);
    }
}
