using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Enums;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var service = Startup.Services.GetService<IWeatherEventService>();
            LoadData(service);
        }

        private async void BtnSeeHistory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryEventsPage());
        }

        private async void LoadData(IWeatherEventService eventService)
        {
            var lastEvent = await eventService.GetLast();
            
            lbl_lastUpdate.Text = $"Last update at {lastEvent.EventTime.ToLongDateString()}";
            lbl_temperature.Text = $"{lastEvent.TemperatureInCelsius}°C";
            lbl_humidity.Text = $"{lastEvent.Humidity}%";
            lbl_temperatureMax.Text = $"{lastEvent.HeatIndexInCelsius}°C";

            lastEventsList.ItemsSource = await GetLastEvents(eventService);

            loaderLayout.IsVisible = false;
            mainLayout.IsVisible = true;
        }

        private async static Task<IEnumerable<EventModel>> GetLastEvents(IWeatherEventService eventService)
        {
            var lastEvents = new List<EventModel>(4);

            var events = await eventService.GetHistoryIn(TimeInterval.LastWeek);

            for (int i = 1; i <= 4; i++)
            {
                var dayAgo = DateTime.Now.Subtract(TimeSpan.FromDays(i));

                var lastEventAtDay = events
                    .AsQueryable()
                    .Where(e => e.EventTime > dayAgo && e.EventTime.Day == dayAgo.Day)
                    .OrderByDescending(e => e.EventTime)
                    .FirstOrDefault();

                lastEvents.Add(lastEventAtDay);
            }

            return lastEvents;
        }
    }
}