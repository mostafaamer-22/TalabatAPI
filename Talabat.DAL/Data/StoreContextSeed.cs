using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order;

namespace Talabat.DAL.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context , ILoggerFactory loggerFactory)
        {
            try
            {

                if(!context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Talabat.DAL/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    foreach (var brand in brands)
                    {
                        await context.ProductBrands.AddAsync(brand);
                    }
                    await context.SaveChangesAsync();
                }
                if(!context.ProductTypes.Any())
                {
                    var types = File.ReadAllText("../Talabat.DAL/Data/SeedData/types.json");
                    var data = JsonSerializer.Deserialize<List<ProductType>>(types);
                    foreach(var type in data)
                    {
                        await context.ProductTypes.AddAsync(type);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var products = File.ReadAllText("../Talabat.DAL/Data/SeedData/products.json");
                    var data = JsonSerializer.Deserialize<List<Product>>(products);
                    foreach (var product in data)
                    {
                        await context.Products.AddAsync(product);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.DeliveryMethods.Any())
                {
                    var delivery = File.ReadAllText("../Talabat.DAL/Data/SeedData/delivery.json");
                    var data = JsonSerializer.Deserialize<List<DeliveryMethod>>(delivery);
                    foreach (var item in data)
                    {
                        await context.DeliveryMethods.AddAsync(item);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch(Exception ex)
            {
                var Logger = loggerFactory.CreateLogger<StoreContextSeed>();
                Logger.LogError(ex.Message);
            }
        }
    }
}
