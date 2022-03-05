using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Enums;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherEventService
    {
        Task<EventModel> GetLast();
        Task<IReadOnlyList<EventModel>> GetHistoryIn(TimeInterval timeInterval);
    }
}
