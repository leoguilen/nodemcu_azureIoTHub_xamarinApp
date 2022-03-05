using System;
using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class EventModel
    {
        [JsonPropertyName("eventTime")]
        public DateTime EventTime { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }

        [JsonPropertyName("temperatureInCelsius")]
        public double TemperatureInCelsius { get; set; }

        [JsonPropertyName("temperatureInFahrenheit")]
        public double TemperatureInFahrenheit { get; set; }

        [JsonPropertyName("heatIndexInCelsius")]
        public double HeatIndexInCelsius { get; set; }

        [JsonPropertyName("heatIndexInFahrenheit")] 
        public double HeatIndexInFahrenheit { get; set; }
    }
}
