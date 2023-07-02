using Elastic.Clients.Elasticsearch;
using ElasticSearch.Api.DTOs;
using ElasticSearch.Api.Models;

namespace ElasticSearch.Api.Repository
{
	public class ProductRepository
    {
        private readonly ElasticsearchClient _client;

        public ProductRepository(ElasticsearchClient client)
        {
            _client = client;
        }
        public async Task<Product?> SaveAsync(Product product)
        {
            product.Created = DateTime.Now;
            var response = await _client.IndexAsync(product, x => x.Index("products"));

            if (!response.IsValidResponse)
            {
                return null;
            }
            product.Id = response.Id;
            return product;


        }

        public async Task<IReadOnlyCollection<Product>> GetAll()
        {
            var result = await _client.SearchAsync<Product>(s => s.Index("products").Query(q => q.MatchAll()));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }
            return result.Documents;
        }

        public async Task<Product> GetById(string id)
        {
            var result = await _client.GetAsync<Product>(id,x=>x.Index("products"));

            result.Source.Id=result.Id;
            return result.Source;
        }

        public async Task<bool> Update(ProductUpdateDto updateDto)
        {
            var response = await _client.UpdateAsync<Product, ProductUpdateDto>("products",updateDto.id);

            return response.IsValidResponse;
        }
        public async Task<bool> Delete(string id)
        {
            var response = await _client.DeleteAsync<Product>(id, x => x.Index("products"));

            return response.IsValidResponse;
        }
    }
}
