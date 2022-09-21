using Newtonsoft.Json.Linq;

namespace gov.minahasa.sitimou.Helper.WeatherWebAPI
{
    public class Weather
    {
        public readonly int Id;
        public readonly string Main;
        public readonly string Description;
        public readonly string Icon;

        public Weather(JToken weatherData)
        {
            Id = int.Parse(weatherData.SelectToken("id").ToString());
            Main = weatherData.SelectToken("main").ToString();
            Description = weatherData.SelectToken("description").ToString();
            Icon = weatherData.SelectToken("icon").ToString();
        }
    }
}
