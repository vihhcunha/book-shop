using AutoMapper;
using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Interfaces.Notifications;
using Book_Shop.Business.Interfaces.Services;
using Book_Shop.Business.Models;
using Book_Shop.Web.Extensions;
using Book_Shop.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Web.Controllers
{
    [Route("providers")]
    [Authorize]
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, 
            IMapper mapper, 
            IAddressRepository addressRepository, 
            IProviderService providerService,
            INotificator notificator) : base(notificator)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _providerService = providerService;
        }

        [Route("providers-list")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var providerList = await _providerRepository.GetAll();
            var providerListViewModel = _mapper.Map<List<ProviderViewModel>>(providerList);
            return View(providerListViewModel);
        }

        [Route("provider/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [Route("new-provider")]
        [ClaimsAuthorize("Provider", "Add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("new-provider")]
        [ClaimsAuthorize("Provider", "Add")]
        public async Task<IActionResult> Create(ProviderViewModel providerViewModel)
        {
            if (!ModelState.IsValid) return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Add(provider);

            if (!ValidOperation()) return View(providerViewModel);

            return RedirectToAction(nameof(Index));
        }

        [Route("edit-provider/{id:guid}")]
        [ClaimsAuthorize("Provider", "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetProviderProductsAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }
            return View(providerViewModel);
        }

        [HttpPost]
        [Route("edit-provider/{id:guid}")]
        [ClaimsAuthorize("Provider", "Edit")]
        public async Task<IActionResult> Edit(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Update(provider);

            if (!ValidOperation()) return View(await GetProviderProductsAddress(id));

            return RedirectToAction(nameof(Index));
        }

        [Route("delete-provider/{id:guid}")]
        [ClaimsAuthorize("Provider", "Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("delete-provider/{id:guid}")]
        [ClaimsAuthorize("Provider", "Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if(providerViewModel == null) return NotFound();

            await _providerService.Remove(id);

            if (!ValidOperation()) return View(providerViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("edit-address/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if (provider == null)
            {
                return NotFound();
            }

            return PartialView("_UpdateAddress", new ProviderViewModel { Address = provider.Address });
        }

        [HttpPost]
        [Route("edit-address/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(ProviderViewModel providerViewModel)
        {
            ModelState.Remove(nameof(providerViewModel.Name));
            ModelState.Remove(nameof(providerViewModel.Document));
            if (!ModelState.IsValid) return PartialView("_UpdateAddress", providerViewModel);

            var address = _mapper.Map<Address>(providerViewModel.Address);
            await _providerService.UpdateAddress(address);

            if (!ValidOperation()) return View(providerViewModel);

            var url = Url.Action("GetAddress", "Providers", new { id = providerViewModel.Address.ProviderId });
            return Json(new { success = true, url });
        }

        public async Task<IActionResult> GetAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if (provider == null)
            {
                return NotFound();
            }
            return PartialView("_DetailsAddress", provider);
        }

        private async Task<ProviderViewModel> GetProviderAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderAddress(id));
        }

        private async Task<ProviderViewModel> GetProviderProductsAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderProductsAddress(id));
        }
    }
}
