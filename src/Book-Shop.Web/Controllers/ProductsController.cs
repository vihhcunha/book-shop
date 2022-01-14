using AutoMapper;
using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Models;
using Book_Shop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper, IProviderRepository providerRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _providerRepository = providerRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var productViewModel = await PopulateProviders(new ProductViewModel());

            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await PopulateProviders(productViewModel);
            if (!ModelState.IsValid) return View(productViewModel);

            await _productRepository.Add(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            productViewModel = await PopulateProviders(productViewModel);
            if (!ModelState.IsValid) return View(productViewModel);

            await _productRepository.Update(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.Remove(id);

            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var produto = _mapper.Map<ProductViewModel>(await _productRepository.GetProductProvider(id));
            produto.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            return produto;
        }

        private async Task<ProductViewModel> PopulateProviders(ProductViewModel produto)
        {
            produto.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            return produto;
        }
    }
}
