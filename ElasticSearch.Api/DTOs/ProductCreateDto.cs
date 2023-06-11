using ElasticSearch.Api.Models;

namespace ElasticSearch.Api.DTOs
{
    public record ProductCreateDto(string Name,decimal Price,int Stock,ProductFeature ProductFeature)
    {
        public Product CreateProduct()
        {
            return new Product { Name=Name, Price=Price,stock=Stock,
                ProductFeature=new ProductFeature
            {
                
                Width = ProductFeature.Width,
                Height = ProductFeature.Height,
                Color = ProductFeature.Color,
            }

            };
            }
    }

    public record ProductUpdateDto(string id,string Name, decimal Price, int Stock, ProductFeature ProductFeature)
    {
       
    }
}
