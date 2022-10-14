
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
namespace minahasa.sitimou.webapi.Helper;
// using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RestSharp;

public class FcmHelper : DbConnection
{
    private const string FcmServerId = "AAAAZO6mjQk:APA91bEraA8pKOWNZt-5YmsFs__QnuYd_ll2rlR92wVT8izv7hqEbK05ax1Kwe2DIn9j-IzfLQzx1RdniguxJBXyabDXonWmn4J35MgObaVnu9RtiliL-8gD9HOD6VJrChhJsUZ3QU1J";

    public static void SendFcmNotification(string toToken, string notifTitle, string notifBody, int notifUserId = 0)
    {
        try
        {
            // Prep notifikasi payload
            var payload = new
            {
                to = toToken,
                data = new
                {
                    title = notifTitle,
                    body = notifBody,
                    userid = notifUserId
                },
                priority = "high"
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);
                
            Console.WriteLine(jsonPayload);
            
            // Kirim notifikasi
            var client = new RestClient("https://fcm.googleapis.com/fcm/send")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"key={FcmServerId}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonPayload, ParameterType.RequestBody);
            var response = client.Execute(request);
            
            Console.WriteLine($"SendNotification [response]: {response.Content}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
            

    }
}