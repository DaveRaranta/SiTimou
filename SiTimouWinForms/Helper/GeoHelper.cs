using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using gov.minahasa.sitimou.Models;

namespace gov.minahasa.sitimou.Helper
{
    internal class GeoHelper
    {
        public static RootObject FindAddress(double lat, double lon)
        {

            var webClient = new WebClient();

            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webClient.Headers.Add("Referer", "http://www.microsoft.com");
            
            var url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}";

            var jsonData = webClient.DownloadData(url.Replace(",", "."));


            var ser = new DataContractJsonSerializer(typeof(RootObject));
            var rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));
            return rootObject;

        }
    }
}
