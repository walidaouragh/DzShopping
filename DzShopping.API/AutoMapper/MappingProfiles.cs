using AutoMapper;
using DzShopping.API.Dtos;
using DzShopping.API.Helpers;
using DzShopping.Core.Models;

namespace DzShopping.API.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(p => p.ProductBrand, opt => opt.MapFrom(from => from.ProductBrand.ProductBrandName))
                .ForMember(p => p.ProductType, opt => opt.MapFrom(from => from.ProductType.ProductTypeName))

                //This is where we apply custom value resolver for autoMapper
                .ForMember(p => p.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());
        }
    }
}
