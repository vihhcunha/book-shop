using AutoMapper;
using Book_Shop.Business.Models;
using Book_Shop.Web.ViewModels;

namespace Book_Shop.Web.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
            CreateMap<ProductViewModel, ProductViewModel>().ReverseMap();
            CreateMap<AddressViewModel, AddressViewModel>().ReverseMap();
        }
    }
}
