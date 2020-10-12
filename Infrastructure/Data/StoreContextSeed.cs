using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory logger)
        {
            // test si ProducBrand est vide
            if(!context.ProductBrands.Any())
            {
                //retourne le fichier json dans une chaine
                var brandData = File.ReadAllText("../Infrastructure/Data/Seed/brands.json");
                // transformation de la chaine en liste
                // et Deserialisation dans la liste 
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                foreach(var data in brands)
                {
                    context.ProductBrands.Add(data);
                }
                await context.SaveChangesAsync();
            }
            //test si table vide
            if(!context.ProductTypes.Any())
            {
                //retourne le fichier json dans une chaine
                var typeData = File.ReadAllText("../Infrastructure/Data/Seed/types.json");
                // transformation de la chaine en liste
                // et Deserialisation dans la liste 
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                foreach(var data in types)
                {
                    context.ProductTypes.Add(data);
                }
                await context.SaveChangesAsync();
            }
            //test si table vide
            if(!context.Products.Any())
            {
                //retourne le fichier json dans une chaine
                var productData = File.ReadAllText("../Infrastructure/Data/Seed/products.json");                
                // transformation de la chaine en liste
                // et Deserialisation dans la liste 
                var types = JsonSerializer.Deserialize<List<Product>>(productData);
                foreach(var data in types)
                {
                    context.Products.Add(data);
                }
                await context.SaveChangesAsync();
            }
            
        }
    }
}