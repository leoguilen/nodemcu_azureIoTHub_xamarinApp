using FunctionQuery.Configurations;
using FunctionQuery.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FunctionQuery.Data.Context
{
    public class MongoDbContext
    {
        private readonly string _connectionStrings;
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbConfig> mongoConfig)
        {
            _connectionStrings = mongoConfig.Value.ConnectionString;
            _databaseName = mongoConfig.Value.DatabaseName;
            _collectionName = mongoConfig.Value.CollectionName;

            _client = new MongoClient(_connectionStrings);
            _database = _client.GetDatabase(_databaseName);
        }

        public IMongoClient Client => _client;
        public IMongoDatabase Database => _database;
        public IMongoCollection<EventModel> Events => _database.GetCollection<EventModel>(_collectionName);
    }
}
