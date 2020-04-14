using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DzShopping.Core.Models;
using DzShopping.Infrastructure.DbContext;
using Microsoft.Extensions.Logging;

namespace DzShopping.Infrastructure
{
    public class DzContextSeed
    {
        public static async Task SeedDzDb(DzDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData =
                        File.ReadAllText("../DzShopping.Infrastructure/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands) context.ProductBrands.Add(item);

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData =
                        File.ReadAllText("../DzShopping.Infrastructure/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types) context.ProductTypes.Add(item);

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText("../DzShopping.Infrastructure/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products) context.Products.Add(item);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DzContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}