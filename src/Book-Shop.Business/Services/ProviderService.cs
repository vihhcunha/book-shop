using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Interfaces.Notifications;
using Book_Shop.Business.Interfaces.Services;
using Book_Shop.Business.Models;
using Book_Shop.Business.Validations;

namespace Book_Shop.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository, INotificator notificator) : base(notificator)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }

        public async Task Add(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider) || !ExecuteValidation(new AddressValidation(), provider.Address)) return;

            if (_providerRepository.Search(_ => _.Document == provider.Document).Result.Any())
            {
                Notificate("Already have a provider with the same document. Please insert a new one!", nameof(Provider.Document));
                return;
            }
            
            await _providerRepository.Add(provider);
        }

        public async Task Remove(Guid id)
        {
            if (_providerRepository.GetProviderProductsAddress(id).Result.Products.Any())
            {
                Notificate("The provider have products. Delete all products first.");
                return;
            }

            await _providerRepository.Remove(id);
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider)) return;

            if (_providerRepository.Search(_ => _.Document == provider.Document && _.Id != provider.Id).Result.Any())
            {
                Notificate("Already have a provider with the same document. Please insert a new one!");
                return;
            }

            await _providerRepository.Update(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }

        public void Dispose()
        {
            _providerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
