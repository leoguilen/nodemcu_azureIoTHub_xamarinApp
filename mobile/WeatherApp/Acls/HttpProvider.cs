using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using WeatherApp.PollyPolices;

namespace WeatherApp.Acls
{
    public class HttpProvider
    {
        private static bool _flurlConfigured;

        public HttpProvider()
        {
            ConfigureFlurl();
        }

        public async Task<TResponse> GetAsync<TResponse>(string requestUri, string queryParams = null)
        {
            return await BuilderGetAsync(requestUri, queryParams)
                .ReceiveJson<TResponse>();
        }

        private static void ConfigureFlurl()
        {
            if (_flurlConfigured)
            {
                return;
            }

            FlurlHttp.Configure(config =>
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                };
                config.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
                config.HttpClientFactory = new PollyHttpClientFactory();
            });

            _flurlConfigured = true;
        }

        private async Task<IFlurlResponse> BuilderGetAsync(string requestUri, string queryParams)
            => string.IsNullOrWhiteSpace(queryParams) ?
                await requestUri.GetAsync() :
                await requestUri
                    .SetQueryParam("timeInterval", queryParams)
                    .GetAsync();
    }
}
