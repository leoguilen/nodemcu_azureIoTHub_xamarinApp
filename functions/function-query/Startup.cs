using FunctionQuery;
using FunctionQuery.Configurations;
using FunctionQuery.Data.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionQuery
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddOptions<MongoDbConfig>()
                .Configure<IConfiguration>((mongoDbConfig, configuration) =>
                {
                    mongoDbConfig.ConnectionString = configuration["MongoDbConnectionString"];
                    mongoDbConfig.DatabaseName = configuration["MongoDbDatabaseName"];
                    mongoDbConfig.CollectionName = configuration["MongoDbCollectionName"];
                });

            builder.Services.AddScoped<IEventRepository, EventRepository>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var configuration = builder.ConfigurationBuilder.Build();

            var keyVaultEndpoint = configuration["KEYVAULT_ENDPOINT"];
            if (string.IsNullOrWhiteSpace(keyVaultEndpoint))
            {
                builder.ConfigurationBuilder
                   .SetBasePath(Environment.CurrentDirectory)
                   .AddJsonFile("local.settings.json", true)
                   .AddUserSecrets(Assembly.GetExecutingAssembly())
                   .AddEnvironmentVariables()
                   .Build();

                return;
            }

            builder.ConfigurationBuilder
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddAzureKeyVault(keyVaultEndpoint)
                    .AddJsonFile("local.settings.json", true)
                    .AddEnvironmentVariables()
                .Build();
        }
    }
}
