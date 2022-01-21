using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Interfaces.Notifications;
using Book_Shop.Business.Interfaces.Services;
using Book_Shop.Business.Models;
using Book_Shop.Business.Validations;

namespace Book_Shop.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Add(product);
        }

        public async Task Remove(Guid id)
        {
            await _productRepository.Remove(id);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
