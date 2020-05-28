using AutoMapper;
using DzShopping.API.Dtos;
using DzShopping.Core.Models;
using DzShopping.Core.Models.OrderAggregate;

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

            CreateMap<Core.Models.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerCartDto, CustomerCart>();
            CreateMap<CartItemDto, CartItem>();

            CreateMap<AddressDto, Core.Models.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
               .ForMember(d => d.DeliveryMethod, opt => opt.MapFrom(from => from.DeliveryMethod.ShortName))
               .ForMember(d => d.ShippingPrice, opt => opt.MapFrom(from => from.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, opt => opt.MapFrom(from => from.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, opt => opt.MapFrom(from => from.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, opt => opt.MapFrom(from => from.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, opt => opt.MapFrom<OrderItemUrlResolver>()); 
        }
    }
} 
