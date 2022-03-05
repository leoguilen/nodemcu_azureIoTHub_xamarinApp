using System;
using WeatherApp.Enums;
using WeatherApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryEventsPage : ContentPage
    {
        private const string SelectedStyle = "Selected";
        private const string UnselectedStyle = "Unselected";

        private readonly IWeatherEventService _weatherEventService;

        private TimeInterval _timeInterval = TimeInterval.LastDay;

        public HistoryEventsPage()
        {
            InitializeComponent();
            _weatherEventService = Startup.Services.GetService<IWeatherEventService>();
            LoadHistory(TimeInterval.LastDay);
        }

        private async void LoadHistory(TimeInterval timeInterval)
        {
            var events = await _weatherEventService.GetHistoryIn(timeInterval);
            historyEventsList.ItemsSource = events;
        }

        private void BtnLastDay_Clicked(object sender, EventArgs e)
        {
            if (BtnLastDay.StyleClass.Contains(SelectedStyle))
            {
                return;
            }

            BtnLastDay.StyleClass = new[] { SelectedStyle };
            BtnLastWeek.StyleClass = new[] { UnselectedStyle };
            BtnLastMonth.StyleClass = new[] { UnselectedStyle };

            historyEventsList.ItemsSource = null;
            LoadHistory(TimeInterval.LastDay);
            _timeInterval = TimeInterval.LastDay;
        }

        private void BtnLastWeek_Clicked(object sender, EventArgs e)
        {
            if (BtnLastWeek.StyleClass.Contains(SelectedStyle))
            {
                return;
            }

            BtnLastDay.StyleClass = new[] { UnselectedStyle };
            BtnLastWeek.StyleClass = new[] { SelectedStyle };
            BtnLastMonth.StyleClass = new[] { UnselectedStyle };

            historyEventsList.ItemsSource = null;
            LoadHistory(TimeInterval.LastWeek);
            _timeInterval = TimeInterval.LastWeek;
        }

        private void BtnLastMonth_Clicked(object sender, EventArgs e)
        {
            if (BtnLastMonth.StyleClass.Contains(SelectedStyle))
            {
                return;
            }

            BtnLastDay.StyleClass = new[] { UnselectedStyle };
            BtnLastWeek.StyleClass = new[] { UnselectedStyle };
            BtnLastMonth.StyleClass = new[] { SelectedStyle };

            historyEventsList.ItemsSource = null;
            LoadHistory(TimeInterval.LastMonth);
            _timeInterval = TimeInterval.LastMonth;
        }

        private void historyEventsList_Refreshing(object sender, EventArgs e)
        {
            LoadHistory(_timeInterval);
            historyEventsList.IsRefreshing = false;
        }
    }
}