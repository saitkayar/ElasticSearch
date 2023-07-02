using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticSearch.Api.Extensions
{
	public static class Elasticsearch
    {
        public static void AddElastic(this IServiceCollection services,IConfiguration configuration)
        {

			#region nest
			//var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));

			//var settings = new ConnectionSettings(pool);

			//var client = new ElasticClient(settings); 
			#endregion
			string userName = configuration.GetSection("Elastic")["UserName"]!;
			string password = configuration.GetSection("Elastic")["Password"]!;
			var settings=new ElasticsearchClientSettings(new Uri(configuration.GetValue<string>("Elastic:Url")!)).Authentication(new BasicAuthentication(userName,password));

              
            var client = new ElasticsearchClient(settings);


           services.AddSingleton(client);
        }
    }
}
