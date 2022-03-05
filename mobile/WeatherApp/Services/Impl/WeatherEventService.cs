using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Acls;
using WeatherApp.Configurations;
using WeatherApp.Enums;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherEventService : IWeatherEventService
    {
        private readonly HttpProvider _httpProvider;
        private readonly ApiConfig _apiConfig;

        public WeatherEventService(HttpProvider httpProvider, ApiConfig apiConfig)
        {
            _httpProvider = httpProvider;
            _apiConfig = apiConfig;
        }

        public async Task<IReadOnlyList<EventModel>> GetHistoryIn(TimeInterval timeInterval)
        {
            var requestUri = string.Join("/", _apiConfig.BaseUrl, _apiConfig.Routes[0]);
            return await _httpProvider.GetAsync<IReadOnlyList<EventModel>>(requestUri, timeInterval.ToString());
        }

        public async Task<EventModel> GetLast()
        {
            var requestUri = string.Join("/", _apiConfig.BaseUrl, _apiConfig.Routes[1]);
            return await _httpProvider.GetAsync<EventModel>(requestUri);
        }
    }
}
