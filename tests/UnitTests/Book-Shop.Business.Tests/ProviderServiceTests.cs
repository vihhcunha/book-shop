using Book_Shop.Business.Interfaces.Notifications;
using Book_Shop.Business.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Book_Shop.Business.Tests
{
    public class ProviderServiceTests : ServiceTestsBase
    {
        private Provider _providerIndividualEntity;
        private Provider _providerLegalEntity;

        public ProviderServiceTests()
        {
            var address = new Address
            {
                City = "São Paulo",
                Complement = "",
                District = "Centro",
                Id = Guid.NewGuid(),
                Number = "50",
                State = "SP",
                Street = "Rua 1",
                ZipCode = "00044100"
            };

            _providerIndividualEntity = new Provider
            {
                Id = Guid.NewGuid(),
                Active = true,
                Address = address,
                Document = "92301883019",
                ProviderKind = ProviderKind.IndividualEntity,
                Name = "Provider Test"
            };

            _providerLegalEntity = new Provider
            {
                Id = Guid.NewGuid(),
                Active = true,
                Address = address,
                Document = "76809035000138",
                ProviderKind = ProviderKind.LegalEntity,
                Name = "Provider Test Legal"
            };
        }

        [Fact(DisplayName = "Add provider - Must add a individual entity provider")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddIndividualEntityProvider_MustAddProvider()
        {
            await ProviderService.Add(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.False(notificator.HasNotification);
        }

        [Fact(DisplayName = "Add provider - Must add a legal entity provider")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddLegalEntityProvider_MustAddProvider()
        {
            await ProviderService.Add(_providerLegalEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.False(notificator.HasNotification);
        }

        [Fact(DisplayName = "Add provider - Must validate empty provider name")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddProviderWithEmptyName_MustValidate()
        {
            _providerIndividualEntity.Name = string.Empty;
            await ProviderService.Add(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Name));
        }

        [Fact(DisplayName = "Add provider - Must validate minimum caracter length provider name")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddProviderNameWithMinimumNumberOfCaracters_MustValidate()
        {
            _providerIndividualEntity.Name = "a";
            await ProviderService.Add(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Name));
        }

        [Fact(DisplayName = "Add provider - Must validate maximum caracter length provider name")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddProviderNameWithMaximumNumberOfCaracters_MustValidate()
        {
            _providerIndividualEntity.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam maximus consequat lacinia. Aenean sed venenatis neque, in sodales nisi. Aenean semper quam at volutpat elementum. Curabitur fermentum pretium sem. Duis viverra, leo non egestas venenatis, risus sem molestie augue, in lacinia neque ante id lectus. Donec sed egestas est. Vivamus consectetur orci id ex posuere fermentum. Suspendisse potenti. Aliquam sollicitudin at nibh a posuere. Nam viverra euismod magna, ut tincidunt magna placerat eget. Etiam sagittis commodo dapibus. Donec iaculis consectetur sapien, semper laoreet ante porttitor tristique. Curabitur sit amet vestibulum felis.Morbi condimentum, ante sit amet mollis eleifend, urna lorem viverra tellus, ut ullamcorper neque dolor quis nunc. Quisque efficitur ac nunc sit amet efficitur.In elit nibh, mattis at placerat in, ornare non est.Morbi sed dictum risus. Curabitur feugiat quam at rhoncus imperdiet. Donec gravida mauris id elit blandit vehicula.Fusce aliquam euismod neque sed consequat. Donec consectetur vehicula nulla vitae mollis. Aenean euismod dapibus sapien.";
            await ProviderService.Add(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Name));
        }

        [Fact(DisplayName = "Add provider - Must validate size of legal entity document")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddProviderWithLegalDocumentWithWrongSize_MustValidate()
        {
            _providerLegalEntity.Document = "111";
            await ProviderService.Add(_providerLegalEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerLegalEntity.Document));
        }

        [Fact(DisplayName = "Add provider - Must validate legal entity document")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddProviderWithLegalDocumentWithWrongFormat_MustValidate()
        {
            _providerLegalEntity.Document = "0000aaa5000000";
            await ProviderService.Add(_providerLegalEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerLegalEntity.Document));
        }

        [Fact(DisplayName = "Add provider - Must validate size of individual entity document")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddProviderWithIndividualDocumentWithWrongSize_MustValidate()
        {
            _providerIndividualEntity.Document = "111";
            await ProviderService.Add(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Document));
        }

        [Fact(DisplayName = "Add provider - Must validate individual entity document")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddProviderWithIndividualDocumentWithWrongFormat_MustValidate()
        {
            _providerIndividualEntity.Document = "00009035000000";
            await ProviderService.Add(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Document));
        }

        [Fact(DisplayName = "Add provider - Must validate address")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_AddProviderWithWrongAddress_MustValidate()
        {
            _providerIndividualEntity.Address = new Address();
            await ProviderService.Add(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Equal(6, notificator.GetNotifications().Count);
        }

        [Fact(DisplayName = "Edit provider - Must add a individual entity provider")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditIndividualEntityProvider_MustAddProvider()
        {
            await ProviderService.Update(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.False(notificator.HasNotification);
        }

        [Fact(DisplayName = "Edit provider - Must add a legal entity provider")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditLegalEntityProvider_MustAddProvider()
        {
            await ProviderService.Update(_providerLegalEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.False(notificator.HasNotification);
        }

        [Fact(DisplayName = "Edit provider - Must validate empty provider name")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditProviderWithEmptyName_MustValidate()
        {
            _providerIndividualEntity.Name = string.Empty;
            await ProviderService.Update(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Name));
        }

        [Fact(DisplayName = "Edit provider - Must validate minimum caracter length provider name")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditProviderNameWithMinimumNumberOfCaracters_MustValidate()
        {
            _providerIndividualEntity.Name = "a";
            await ProviderService.Update(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Name));
        }

        [Fact(DisplayName = "Edit provider - Must validate maximum caracter length provider name")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditProviderNameWithMaximumNumberOfCaracters_MustValidate()
        {
            _providerIndividualEntity.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam maximus consequat lacinia. Aenean sed venenatis neque, in sodales nisi. Aenean semper quam at volutpat elementum. Curabitur fermentum pretium sem. Duis viverra, leo non egestas venenatis, risus sem molestie augue, in lacinia neque ante id lectus. Donec sed egestas est. Vivamus consectetur orci id ex posuere fermentum. Suspendisse potenti. Aliquam sollicitudin at nibh a posuere. Nam viverra euismod magna, ut tincidunt magna placerat eget. Etiam sagittis commodo dapibus. Donec iaculis consectetur sapien, semper laoreet ante porttitor tristique. Curabitur sit amet vestibulum felis.Morbi condimentum, ante sit amet mollis eleifend, urna lorem viverra tellus, ut ullamcorper neque dolor quis nunc. Quisque efficitur ac nunc sit amet efficitur.In elit nibh, mattis at placerat in, ornare non est.Morbi sed dictum risus. Curabitur feugiat quam at rhoncus imperdiet. Donec gravida mauris id elit blandit vehicula.Fusce aliquam euismod neque sed consequat. Donec consectetur vehicula nulla vitae mollis. Aenean euismod dapibus sapien.";
            await ProviderService.Update(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Name));
        }

        [Fact(DisplayName = "Edit provider - Must validate size of legal entity document")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditProviderWithLegalDocumentWithWrongSize_MustValidate()
        {
            _providerLegalEntity.Document = "111";
            await ProviderService.Update(_providerLegalEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerLegalEntity.Document));
        }

        [Fact(DisplayName = "Edit provider - Must validate legal entity document")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditProviderWithLegalDocumentWithWrongFormat_MustValidate()
        {
            _providerLegalEntity.Document = "0000aaa5000000";
            await ProviderService.Update(_providerLegalEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerLegalEntity.Document));
        }

        [Fact(DisplayName = "Edit provider - Must validate size of individual entity document")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditProviderWithIndividualDocumentWithWrongSize_MustValidate()
        {
            _providerIndividualEntity.Document = "111";
            await ProviderService.Update(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Document));
        }

        [Fact(DisplayName = "Edit provider - Must validate individual entity document")]
        [Trait("Category", "ProviderServices")]
        public async Task ProviderServices_EditProviderWithIndividualDocumentWithWrongFormat_MustValidate()
        {
            _providerIndividualEntity.Document = "00009035000000";
            await ProviderService.Update(_providerIndividualEntity);
            var notificator = ServiceProvider.GetService<INotificator>();

            Assert.True(notificator.HasNotification);
            Assert.Contains(notificator.GetNotifications(), _ => _.PropertyName == nameof(_providerIndividualEntity.Document));
        }
    }
}
