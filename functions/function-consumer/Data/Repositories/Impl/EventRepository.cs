using FunctionConsumer.Configurations;
using FunctionConsumer.Data.Context;
using FunctionConsumer.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FunctionConsumer.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly MongoDbContext _dbContext;

        public EventRepository(IOptions<MongoDbConfig> mongoConfig) =>
            _dbContext = new MongoDbContext(mongoConfig);

        public async Task AddAsync(EventModel @event) => 
            await _dbContext.Events.InsertOneAsync(@event);
    }
}
