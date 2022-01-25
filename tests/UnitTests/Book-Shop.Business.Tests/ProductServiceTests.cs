using Book_Shop.Business.Interfaces.Notifications;
using Book_Shop.Business.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Book_Shop.Business.Tests
{
    public class ProductServiceTests : ServiceTestsBase
    {
        private Product _product;

        public ProductServiceTests()
        {
            _product = new Product
            {
                Active = true,
                Description = "Desc test",
                Image = "/path/file.png",
                Name = "File_1",
                ProviderId = Guid.NewGuid(),
                RegistrationDate = DateTime.Now,
                Value = 20
            };
        }

        [Fact(DisplayName = "Add product - Must add a product")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_AddProduct_MustAddProduct()
        {
            await ProductService.Add(_product);
            var notificator = ServiceProvider.GetService<INotificator>();
            
            Assert.False(notificator.HasNotification);
        }

        [Fact(DisplayName = "Add product - Must validate empty product name")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_AddProductWithEmptyName_MustValidate()
        {
            _product.Name = string.Empty;
            await ProductService.Add(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Name));
        }

        [Fact(DisplayName = "Add product - Must validate minimum caracter length product name")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_AddProductNameWithMinimumNumberOfCaracters_MustValidate()
        {
            _product.Name = "a";
            await ProductService.Add(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Name));
        }

        [Fact(DisplayName = "Add product - Must validate maximum caracter length product name")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_AddProductNameWithMaximumNumberOfCaracters_MustValidate()
        {
            _product.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam maximus consequat lacinia. Aenean sed venenatis neque, in sodales nisi. Aenean semper quam at volutpat elementum. Curabitur fermentum pretium sem. Duis viverra, leo non egestas venenatis, risus sem molestie augue, in lacinia neque ante id lectus. Donec sed egestas est. Vivamus consectetur orci id ex posuere fermentum. Suspendisse potenti. Aliquam sollicitudin at nibh a posuere. Nam viverra euismod magna, ut tincidunt magna placerat eget. Etiam sagittis commodo dapibus. Donec iaculis consectetur sapien, semper laoreet ante porttitor tristique. Curabitur sit amet vestibulum felis.Morbi condimentum, ante sit amet mollis eleifend, urna lorem viverra tellus, ut ullamcorper neque dolor quis nunc. Quisque efficitur ac nunc sit amet efficitur.In elit nibh, mattis at placerat in, ornare non est.Morbi sed dictum risus. Curabitur feugiat quam at rhoncus imperdiet. Donec gravida mauris id elit blandit vehicula.Fusce aliquam euismod neque sed consequat. Donec consectetur vehicula nulla vitae mollis. Aenean euismod dapibus sapien.";
            await ProductService.Add(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Name));
        }

        [Fact(DisplayName = "Add product - Must validate empty product description")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_AddProductWithEmptyDescritption_MustValidate()
        {
            _product.Description = String.Empty;
            await ProductService.Add(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Description));
        }

        [Fact(DisplayName = "Add product - Must validate minimum caracter length product description")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_AddProductDescriptionWithMinimumNumberOfCaracters_MustValidate()
        {
            _product.Description = "a";
            await ProductService.Add(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Description));
        }

        [Fact(DisplayName = "Add product - Must validate maximum caracter length product description")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_AddProductDescriptionWithMaximumNumberOfCaracters_MustValidate()
        {
            _product.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam maximus consequat lacinia. Aenean sed venenatis neque, in sodales nisi. Aenean semper quam at volutpat elementum. Curabitur fermentum pretium sem. Duis viverra, leo non egestas venenatis, risus sem molestie augue, in lacinia neque ante id lectus. Donec sed egestas est. Vivamus consectetur orci id ex posuere fermentum. Suspendisse potenti. Aliquam sollicitudin at nibh a posuere. Nam viverra euismod magna, ut tincidunt magna placerat eget. Etiam sagittis commodo dapibus. Donec iaculis consectetur sapien, semper laoreet ante porttitor tristique. Curabitur sit amet vestibulum felis.Morbi condimentum, ante sit amet mollis eleifend, urna lorem viverra tellus, ut ullamcorper neque dolor quis nunc. Quisque efficitur ac nunc sit amet efficitur.In elit nibh, mattis at placerat in, ornare non est.Morbi sed dictum risus. Curabitur feugiat quam at rhoncus imperdiet. Donec gravida mauris id elit blandit vehicula.Fusce aliquam euismod neque sed consequat. Donec consectetur vehicula nulla vitae mollis. Aenean euismod dapibus sapien.";
            await ProductService.Add(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Description));
        }

        [Fact(DisplayName = "Add product - Must validate value length less or equal than 0")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_AddProductDescriptionWithMaximumNumberOfCaracters_MustValidateIfLessOrEqualThan0()
        {
            _product.Value = 0;
            await ProductService.Add(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Value));
        }

        [Fact(DisplayName = "Edit product - Must edit a product")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_EditProduct_MustEditProduct()
        {
            await ProductService.Update(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.False(notificator.HasNotification);
        }

        [Fact(DisplayName = "Edit product - Must validate empty product name")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_EditProductWithEmptyName_MustValidate()
        {
            _product.Name = string.Empty;
            await ProductService.Update(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Name));
        }

        [Fact(DisplayName = "Edit product - Must validate minimum caracter length product name")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_EditProductNameWithMinimumNumberOfCaracters_MustValidate()
        {
            _product.Name = "a";
            await ProductService.Update(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Name));
        }

        [Fact(DisplayName = "Edit product - Must validate maximum caracter length product name")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_EditProductNameWithMaximumNumberOfCaracters_MustValidate()
        {
            _product.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam maximus consequat lacinia. Aenean sed venenatis neque, in sodales nisi. Aenean semper quam at volutpat elementum. Curabitur fermentum pretium sem. Duis viverra, leo non egestas venenatis, risus sem molestie augue, in lacinia neque ante id lectus. Donec sed egestas est. Vivamus consectetur orci id ex posuere fermentum. Suspendisse potenti. Aliquam sollicitudin at nibh a posuere. Nam viverra euismod magna, ut tincidunt magna placerat eget. Etiam sagittis commodo dapibus. Donec iaculis consectetur sapien, semper laoreet ante porttitor tristique. Curabitur sit amet vestibulum felis.Morbi condimentum, ante sit amet mollis eleifend, urna lorem viverra tellus, ut ullamcorper neque dolor quis nunc. Quisque efficitur ac nunc sit amet efficitur.In elit nibh, mattis at placerat in, ornare non est.Morbi sed dictum risus. Curabitur feugiat quam at rhoncus imperdiet. Donec gravida mauris id elit blandit vehicula.Fusce aliquam euismod neque sed consequat. Donec consectetur vehicula nulla vitae mollis. Aenean euismod dapibus sapien.";
            await ProductService.Update(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Name));
        }

        [Fact(DisplayName = "Edit product - Must validate empty product description")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_EditProductWithEmptyDescritption_MustValidate()
        {
            _product.Description = String.Empty;
            await ProductService.Update(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Description));
        }

        [Fact(DisplayName = "Edit product - Must validate minimum caracter length product description")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_EditProductDescriptionWithMinimumNumberOfCaracters_MustValidate()
        {
            _product.Description = "a";
            await ProductService.Update(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Description));
        }

        [Fact(DisplayName = "Edit product - Must validate maximum caracter length product description")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_EditProductDescriptionWithMaximumNumberOfCaracters_MustValidate()
        {
            _product.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam maximus consequat lacinia. Aenean sed venenatis neque, in sodales nisi. Aenean semper quam at volutpat elementum. Curabitur fermentum pretium sem. Duis viverra, leo non egestas venenatis, risus sem molestie augue, in lacinia neque ante id lectus. Donec sed egestas est. Vivamus consectetur orci id ex posuere fermentum. Suspendisse potenti. Aliquam sollicitudin at nibh a posuere. Nam viverra euismod magna, ut tincidunt magna placerat eget. Etiam sagittis commodo dapibus. Donec iaculis consectetur sapien, semper laoreet ante porttitor tristique. Curabitur sit amet vestibulum felis.Morbi condimentum, ante sit amet mollis eleifend, urna lorem viverra tellus, ut ullamcorper neque dolor quis nunc. Quisque efficitur ac nunc sit amet efficitur.In elit nibh, mattis at placerat in, ornare non est.Morbi sed dictum risus. Curabitur feugiat quam at rhoncus imperdiet. Donec gravida mauris id elit blandit vehicula.Fusce aliquam euismod neque sed consequat. Donec consectetur vehicula nulla vitae mollis. Aenean euismod dapibus sapien.";
            await ProductService.Update(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Description));
        }

        [Fact(DisplayName = "Edit product - Must validate value length less or equal than 0")]
        [Trait("Category", "ProductServices")]
        public async Task ProductServices_EditProductDescriptionWithMaximumNumberOfCaracters_MustValidateIfLessOrEqualThan0()
        {
            _product.Value = 0;
            await ProductService.Update(_product);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_product.Value));
        }
    }
}
