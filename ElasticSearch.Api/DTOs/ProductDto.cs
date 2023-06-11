using ElasticSearch.Api.Models;

namespace ElasticSearch.Api.DTOs
{
    public record ProductDto(string Id,string Name, decimal Price, int Stock, ProductFeatureDto? ProductFeature)
    {
      

      
    }
}
