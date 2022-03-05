using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionConsumer.Models
{
    public class EventModel
    {
        public EventModel()
        {
            Timestamp = DateTime.Now;
        }

        [BsonId]
        [JsonPropertyName("objectId")]
        public ObjectId ObjectId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.String)]
        public DateTime Timestamp { get; private set; }

        [BsonGuidRepresentation(GuidRepresentation.CSharpLegacy)]
        public Guid EventId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.String)]
        public DateTime EventTime { get; set; }
        
        [Key]
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

        [JsonPropertyName("heatIndexInCelsius")]
        public double HeatIndexInCelsius { get; set; }

        [JsonPropertyName("heatIndexInFahrenheit")]
        public double HeatIndexInFahrenheit { get; set; }

        public string ToJson() => JsonSerializer.Serialize(this);
    }
}
