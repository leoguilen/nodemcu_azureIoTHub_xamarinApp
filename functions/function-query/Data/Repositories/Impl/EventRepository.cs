using FunctionQuery.Configurations;
using FunctionQuery.Data.Context;
using FunctionQuery.Enums;
using FunctionQuery.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionQuery.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly MongoDbContext _dbContext;

        public EventRepository(IOptions<MongoDbConfig> mongoConfig) =>
            _dbContext = new MongoDbContext(mongoConfig);

        public async Task<EventModel> GetLastAsync()
        {
            var @event = await _dbContext.Events
                .Find(FilterDefinition<EventModel>.Empty)
                .SortByDescending(x => x.MessageId)
                .FirstOrDefaultAsync();

            return @event;
        }

        public async Task<IEnumerable<EventModel>> GetByTimeintervalAsync(TimeInterval timeInterval)
        {
            var currentDateTime = DateTime.Now;
            var referenceDateTime = timeInterval switch
            {
                TimeInterval.LastDay => currentDateTime.Subtract(TimeSpan.FromDays(1)),
                TimeInterval.LastWeek => currentDateTime.Subtract(TimeSpan.FromDays(7)),
                TimeInterval.LastMonth => currentDateTime.Subtract(TimeSpan.FromDays(30)),
                _ => default
            };

            var events = await _dbContext.Events
                .AsQueryable()
                .ToListAsync();

            return events
                .Where(x => x.EventTime > referenceDateTime)
                .OrderByDescending(x => x.MessageId);
        }
    }
}
