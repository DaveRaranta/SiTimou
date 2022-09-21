using System;
using Newtonsoft.Json.Linq;

namespace gov.minahasa.sitimou.Helper.WeatherWebAPI
{
    public class Main
    {
        public readonly TemperatureObj Temperature;
        public readonly double Pressure;
        public readonly double Humdity;
        public readonly double SeaLevelAtm;
        public readonly double GroundLevelAtm;

        public Main(JToken mainData)
        {
            Temperature = new TemperatureObj(double.Parse(mainData.SelectToken("temp").ToString()),
                double.Parse(mainData.SelectToken("temp_min").ToString()), double.Parse(mainData.SelectToken("temp_max").ToString()));
            Pressure = double.Parse(mainData.SelectToken("pressure").ToString());
            Humdity = double.Parse(mainData.SelectToken("humidity").ToString());
            if (mainData.SelectToken("sea_level") != null)
                SeaLevelAtm = double.Parse(mainData.SelectToken("sea_level").ToString());
            if (mainData.SelectToken("grnd_level") != null)
                GroundLevelAtm = double.Parse(mainData.SelectToken("grnd_level").ToString());
        }

        public class TemperatureObj
        {
            public readonly double Celsius;
            public readonly double Fahrenheit;            
            public readonly double CelsiusMin;
            public readonly double CelsiusMax;
            public readonly double FahrenheitMin;
            public readonly double FahrenheitMax;            

            public TemperatureObj(double temp, double min, double max)
            {
                

                Celsius = convertToCelsius(temp);
                CelsiusMax = convertToCelsius(min);
                CelsiusMin = convertToCelsius(max);

                Fahrenheit = convertToFahrenheit(Celsius);
                FahrenheitMax = convertToFahrenheit(CelsiusMax);
                FahrenheitMin = convertToFahrenheit(CelsiusMin);
            }

            private double convertToFahrenheit(double celsius)
            {
                return Math.Round(((9.0 / 5.0) * celsius) + 32, 3);
            }

            private double convertToCelsius(double kelvin)
            {
                return Math.Round(kelvin - 273.15, 3);
            }
        }
    }
}
