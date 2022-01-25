using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Interfaces.Notifications;
using Book_Shop.Business.Notifications;
using Book_Shop.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Book_Shop.Business.Tests
{
    public class ServiceTestsBase
    {
        protected readonly ProviderService ProviderService;
        protected readonly ProductService ProductService;
        protected readonly ServiceProvider ServiceProvider;

        public ServiceTestsBase()
        {
            var services = new ServiceCollection();
            services.AddScoped<INotificator, Notificator>();

            ServiceProvider = services.BuildServiceProvider();

            ProductService = new ProductService(Mock.Of<IProductRepository>(), ServiceProvider.GetService<INotificator>());
            ProviderService = new ProviderService(Mock.Of<IProviderRepository>(), Mock.Of<IAddressRepository>(), ServiceProvider.GetService<INotificator>());
        }

        public void Dispose()
        {
            
        }
    }
}
