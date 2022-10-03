
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
    private const string FcmServerId = "AAAARZCj6QE:APA91bGjsbsRfIDHAYS_4w-ONIYO8BfKMwzI4ig7pdjho_ojlilQRj6IBTOIGqwdYgowv2h-ZJs6crZ5IuFF7N4sL0FUEUocvCVVRV6PKC-gtX1vgV3Id5x8fbiaI6q-zvHUPzBXekxr";

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