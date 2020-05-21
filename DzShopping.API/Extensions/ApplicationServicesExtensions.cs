using System.Linq;
using DzShopping.API.Errors;
using DzShopping.Infrastructure.Repositories.CartRepository;
using DzShopping.Infrastructure.Repositories.GenericRepository;
using DzShopping.Infrastructure.Repositories.OldFashionRepository.ProductBrandRepository;
using DzShopping.Infrastructure.Repositories.OldFashionRepository.ProductRepository;
using DzShopping.Infrastructure.Repositories.OldFashionRepository.ProductTypeRepository;
using DzShopping.Infrastructure.Services;
using DzShopping.Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DzShopping.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();

            // This for generic repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //To handle endpoint does not exist error request
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            // Nuget package enable retrieving long Jsons
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );

            return services;
        }
    }
}
