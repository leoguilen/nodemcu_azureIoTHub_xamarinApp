﻿using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Startup.ConfigureServices();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
