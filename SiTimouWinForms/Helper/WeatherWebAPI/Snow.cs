using Newtonsoft.Json.Linq;

namespace gov.minahasa.sitimou.Helper.WeatherWebAPI
{
    public class Snow
    {
        public readonly double H3;

        public Snow(JToken snowData)
        {
            if (snowData.SelectToken("3h") != null)
                H3 = double.Parse(snowData.SelectToken("3h").ToString());
        }
    }
}
