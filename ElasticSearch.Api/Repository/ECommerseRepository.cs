using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.Api.Models.ECom;
using System.Collections.Immutable;

namespace ElasticSearch.Api.Repository
{
    public class ECommerseRepository
    {
        private readonly ElasticsearchClient client;

        public ECommerseRepository(ElasticsearchClient client)
        {
            this.client = client;
        }

        private const string indexName = "kibana_sample_data_ecommerce";

        public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {
            //var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));

            //var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.CustomerFirstName.Suffix("keyword"),customerFirstName)));

            var termQuery=new TermQuery("customer_first_name.keyword") { CaseInsensitive = true ,Value=customerFirstName};

            var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termQuery));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

          return  result.Documents.ToImmutableList();
          
        }

        public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstName)
        {
            List<FieldValue> terms = new();
            customerFirstName.ForEach(x =>
            {
                terms.Add(x);
            });

            var termQuery = new TermsQuery() {Field= "customer_first_name.keyword" ,Terms=new TermsQueryField(terms )};

            var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termQuery));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> Prefix(string customerFirstName)
        {
            var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q.Prefix(p=>p.Field(t => t.CustomerFirstName.Suffix("keyword")).Value(customerFirstName))));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> range(double from, double toprice)
        {
            var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q.Range(p => p.NumberRange(t => t.Field(f=>f.TaxfulTotalPrice).Gte(from).Lte(toprice)))));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> matchall()
        {
            var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q =>q.MatchAll()));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> wildcard(string name)
        {
          
            var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q.Wildcard(w=>w.Field(f=>f.CustomerFirstName).Suffix("keyword"))));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

            return result.Documents.ToImmutableList();

        }

        public async Task<ImmutableList<ECommerce>> MatchFullText(string categoriy)
        {
            var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q.Match(m=>m.Field(f=>f.Category).Query(categoriy))));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

            return result.Documents.ToImmutableList();

        }
        public async Task<ImmutableList<ECommerce>> CompoundQuery(string cityname,double price,string category,string menu)
        {
            var result = await client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000)
            .Query(q => q.Bool(m => m.Must(f => f.Term(t=>t.Field("geoip.city.name").Value(cityname)))
            .MustNot(m=>m.Range(r=>r.NumberRange(f=>f.Field(f=>f.TaxfulTotalPrice).Lte(price)))).Should(s=>s.Term(t=>t.Field(f=>f.Category.Suffix("keyword")).Value(category))).Filter(f=>f.Term(t=>t.Field("manufacturer.keyword").Value(menu))))));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

            return result.Documents.ToImmutableList();

        }

    }
}
