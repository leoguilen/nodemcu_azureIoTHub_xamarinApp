using FunctionConsumer.Models;
using System;
using System.Text.Json;

namespace FunctionConsumer.Extensions
{
    public static class StringExtensions
    {
        public static EventModel ToEventModel(this string eventData, string eventId, DateTime eventTime)
        {
            var eventModel = JsonSerializer
                .Deserialize<EventModel>(eventData);

            // Converting utc to local
            var eventTimeLocal = eventTime.Subtract(TimeSpan.FromHours(3));

            eventModel.EventId = Guid.Parse(eventId);
            eventModel.EventTime = eventTimeLocal;
            
            return eventModel;
        }
    }
}
