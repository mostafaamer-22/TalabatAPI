using AutoMapper;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Identity;
using Talabat.Dtos;

namespace Talabat.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>().ForMember(d => d.ProductType, option => option.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductUrlReslover>());
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<Address, UserAddressDto>().ReverseMap();
            CreateMap<DAL.Entities.Order.Address, UserAddressDto>().ReverseMap();
                
        }
    }
}
