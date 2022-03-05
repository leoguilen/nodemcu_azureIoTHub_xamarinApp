namespace WeatherApp.Configurations
{
    public class ApiConfig
    {
        public string BaseUrl { get; set; }
        public string[] Routes { get; set; }
        public int MaxRetries { get; set; }
    }
}
