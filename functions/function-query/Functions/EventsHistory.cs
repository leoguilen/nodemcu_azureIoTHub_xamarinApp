using FunctionQuery.Data.Repositories;
using FunctionQuery.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FunctionQuery.Functions
{
    public class EventsHistory
    {
        private readonly IEventRepository _events;

        public EventsHistory(IEventRepository events) =>
            _events = events;

        [FunctionName("EventsHistory")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest request,
            ILogger logger)
        {
            try
            {
                var timeIntervalQuery = string.IsNullOrEmpty(request.Query["timeInterval"].ToString())
                    ? "LastDay"
                    : request.Query["timeInterval"].ToString();
                var timeInterval = Enum.Parse<TimeInterval>(timeIntervalQuery, true);

                var events = await _events.GetByTimeintervalAsync(timeInterval);

                if (!events.Any())
                {
                    return new NoContentResult();
                }

                logger.LogInformation(
                    "Processed a request :: Events: {@event}",
                    events.Select(x => x.ToJson()));
                return new OkObjectResult(events);
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

