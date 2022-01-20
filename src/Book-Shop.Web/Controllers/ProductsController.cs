using AutoMapper;
using Book_Shop.Business.Interfaces;
using Book_Shop.Business.Models;
using Book_Shop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Web.Controllers
{
    [Route("products")]
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

        [Route("product-list")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders()));
        }

        [Route("product/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [Route("new-product")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await PopulateProviders(new ProductViewModel());

            return View(productViewModel);
        }

        [HttpPost]
        [Route("new-product")]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await PopulateProviders(productViewModel);
            if (!ModelState.IsValid) return View(productViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadFile(productViewModel.ImageUpload, imgPrefixo))
            {
                return View(productViewModel);
            }
            productViewModel.Image = imgPrefixo + productViewModel.ImageUpload.FileName;
            await _productRepository.Add(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
        }

        private async Task<bool> UploadFile(IFormFile imageUpload, string imgPrefixo)
        {
            if (imageUpload.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + imageUpload.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "This file already exists!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageUpload.CopyToAsync(stream);
            }
            return true;
        }

        [Route("edit-product/{id:guid}")]
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
        [Route("edit-product/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();

            var updatedProduct = await GetProduct(id);
            productViewModel.Provider = updatedProduct.Provider;
            productViewModel.Image = updatedProduct.Image;

            if (!ModelState.IsValid) return View(productViewModel);

            if (productViewModel.ImageUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadFile(productViewModel.ImageUpload, imgPrefixo))
                {
                    return View(productViewModel);
                }
                updatedProduct.Image = imgPrefixo + productViewModel.ImageUpload.FileName;
            }
            updatedProduct.Name = productViewModel.Name;
            updatedProduct.Description = productViewModel.Description;
            updatedProduct.Value = productViewModel.Value;
            updatedProduct.Active = productViewModel.Active;

            await _productRepository.Update(_mapper.Map<Product>(updatedProduct));

            return RedirectToAction("Index");
        }

        [Route("delete-product/{id:guid}")]
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
        [Route("delete-product/{id:guid}")]
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
