using AutoMapper;
using DzShopping.API.Dtos;
using DzShopping.Core.Models;
using Microsoft.Extensions.Configuration;

namespace DzShopping.API.Helpers
{
    //Adding custom value resolver for autoMapper
    //IValueResolver is a mapper interface(in my case I have an apiUrl in appSettings.json and I need to add it to the stored url in db to return to the client)
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember,
            ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl)) return _configuration["ApiUrl"] + source.PictureUrl;

            return null;
        }
    }
}
