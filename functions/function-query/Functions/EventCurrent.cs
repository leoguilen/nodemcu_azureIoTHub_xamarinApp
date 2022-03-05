using FunctionQuery.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FunctionQuery.Functions
{
    public class EventCurrent
    {
        private readonly IEventRepository _events;

        public EventCurrent(IEventRepository events) =>
            _events = events;

        [FunctionName("EventCurrent")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest request,
            ILogger logger)
        {
            try
            {
                var @event = await _events.GetLastAsync();

                if (@event is null)
                {
                    return new NoContentResult();
                }

                logger.LogInformation(
                    "Processed a request :: Event: {@event}",
                    @event.ToJson());
                return new OkObjectResult(@event);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new ObjectResult(new
                {
                    ex.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                });
            }
        }
    }
}

