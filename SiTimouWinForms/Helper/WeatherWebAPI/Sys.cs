﻿using System;
using Newtonsoft.Json.Linq;

namespace gov.minahasa.sitimou.Helper.WeatherWebAPI
{
    public class Sys
    {
        public readonly int Type;
        public readonly int Id;
        public readonly double Message;
        public readonly string Country;
        public readonly DateTime Sunrise;
        public readonly DateTime Sunset;

        public Sys(JToken sysData)
        {
            if(sysData.SelectToken("type") != null)
                Type = int.Parse(sysData.SelectToken("type").ToString());
            if(sysData.SelectToken("id") != null)
                Id = int.Parse(sysData.SelectToken("id").ToString());
            //Message = double.Parse(sysData.SelectToken("message").ToString());
            Country = sysData.SelectToken("country").ToString();
            Sunrise = convertUnixToDateTime(double.Parse(sysData.SelectToken("sunrise").ToString()));
            Sunset = convertUnixToDateTime(double.Parse(sysData.SelectToken("sunset").ToString()));
        }

        private DateTime convertUnixToDateTime(double unixTime)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(unixTime).ToLocalTime();
        }
    }
}
