using ElasticSearch.Api.DTOs;
using Nest;

namespace ElasticSearch.Api.Models
{
    public class Product
    {
        [PropertyName("_id")]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; } 
        public int stock { get; set; }

        public DateTime Created { get; set; }
        public DateTime?  Updated { get; set; }
        public ProductFeature?  ProductFeature  { get; set; }

        public ProductDto CreateDto()
        {
            if (ProductFeature is null) return new ProductDto(Id, Name, Price, stock, null);

            return new ProductDto(Id, Name, Price, stock, new ProductFeatureDto(ProductFeature.Width, ProductFeature.Height, ProductFeature.Color));
        }
    }
}
