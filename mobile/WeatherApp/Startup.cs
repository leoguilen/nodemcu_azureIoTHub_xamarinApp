using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WeatherApp.Acls;
using WeatherApp.Configurations;
using WeatherApp.Services;

namespace WeatherApp
{
    public static class Startup
    {
        public static IServiceProvider Services { get; private set; }

        public static IServiceProvider ConfigureServices()
        {
            var configuration = GetConfiguration();

            var apiConfig = configuration
                .GetSection("ApiConfig")
                .Get<ApiConfig>();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(apiConfig)
                .AddSingleton(new HttpProvider())
                .AddScoped<IWeatherEventService, WeatherEventService>()
                .BuildServiceProvider();

            Services = serviceProvider;
            return serviceProvider;
        }

        private static IConfiguration GetConfiguration()
        {
            var appSettingsStream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("WeatherApp.appsettings.json");

            return new ConfigurationBuilder()
                .AddJsonStream(appSettingsStream)
                .Build();
        }
    }
}
