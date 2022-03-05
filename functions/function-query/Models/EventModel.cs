using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FunctionQuery.Models
{
    public class EventModel
    {
        [BsonId]
        [JsonPropertyName("_id")]
        public ObjectId ObjectId { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; private set; }

        [JsonPropertyName("eventId")]
        public Guid EventId { get; set; }

        [JsonPropertyName("eventTime")]
        public DateTime EventTime { get; set; }

        [JsonPropertyName("deviceId")]
        public string DeviceId { get; set; }

        [JsonPropertyName("messageId")]
        public int MessageId { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }

        [JsonPropertyName("tempInCelsius")]
        public double TemperatureInCelsius { get; set; }

        [JsonPropertyName("tempInFahrenheit")]
        public double TemperatureInFahrenheit { get; set; }

        [JsonPropertyName("heatIndexInFahrenheit")]
        public double HeatIndexInCelsius { get; set; }

        [JsonPropertyName("heatIndexInCelsius")]
        public double HeatIndexInFahrenheit { get; set; }

        public string ToJson () => JsonSerializer.Serialize (this);
    }
}