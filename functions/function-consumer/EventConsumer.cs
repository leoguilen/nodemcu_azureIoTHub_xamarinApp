using FunctionConsumer.Constants;
using FunctionConsumer.Data.Repositories;
using FunctionConsumer.Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace FunctionConsumer
{
    public class EventConsumer
    {
        private readonly IEventRepository _events;

        public EventConsumer(IEventRepository events) =>
            _events = events;

        [FunctionName("EventConsumer")]
        public async Task Run([ServiceBusTrigger(ServiceBusConstants.QueueName, Connection = ServiceBusConstants.Connection)]
            string eventData,
            string messageId,
            DateTime enqueuedTimeUtc,
            ILogger logger)
        {
            try
            {
                var eventModel = eventData
                    .ToEventModel(messageId, enqueuedTimeUtc);

                logger.LogInformation("Consumer received event: {@event}", eventModel.ToJson());

                await _events.AddAsync(eventModel);
                
                logger.LogInformation("Event consumed and persisted successfully :: Data: {@event}", eventModel.ToJson());
            }
            catch (MongoException mex)
            {
                logger.LogError(mex, $"{mex.Message} :: Message: {eventData}");
            }
        }
    }
}
