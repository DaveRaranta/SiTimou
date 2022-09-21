namespace gov.minahasa.sitimou.Helper.WeatherWebAPI
{
    public class WeatherApi
    {
        private string _openWeatherApiKey;

        public WeatherApi(string apiKey)
        {
            _openWeatherApiKey = apiKey;
        }

        public void UpdateApiKey(string apiKey)
        {
            _openWeatherApiKey = apiKey;
        }

        //Returns null if invalid request
        public Query Query(string queryStr)
        {
            var newQuery = new Query(_openWeatherApiKey, queryStr);
            return newQuery.ValidRequest ? newQuery : null;
        }
    }
}
