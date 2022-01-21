using Book_Shop.Business.Models;

namespace Book_Shop.Business.Interfaces.Services
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Remove(Guid id);
    }
}
