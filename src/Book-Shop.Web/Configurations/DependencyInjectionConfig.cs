using Book_Shop.Business.Interfaces;
using Book_Shop.Data.Context;
using Book_Shop.Data.Repository;
using Book_Shop.Web.Extensions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace Book_Shop.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BookShopContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoneyValidationAttributeAdapterProvider>();

            return services;
        }
    }
}
