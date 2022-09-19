using RestSharp;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace gov.minahasa.sitimou.Helper
{
    internal class NotifHelper
    {
        private const string SB_FCM_URL = "https://fcm.googleapis.com/fcm/send";

        private const string SB_TITLE_NOTIF = "MITRa FR";

        #region === MessageBox Helper ===

        public void MsgBoxInfo(string pesan, string title = "")
        {
            MessageBox.Show(pesan, title == "" ? SB_TITLE_NOTIF : title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MsgBoxError(string pesan, string title = "")
        {
            MessageBox.Show(pesan, title == "" ? SB_TITLE_NOTIF : title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void MsgBoxWarning(string pesan, string title = "")
        {
            MessageBox.Show(pesan, title == "" ? SB_TITLE_NOTIF : title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public DialogResult MsgBoxQuestion(string pesan, string title = "")
        {
            return MessageBox.Show(pesan, title == "" ? SB_TITLE_NOTIF : title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void MsgBoxSilent(string pesan, string title = "")
        {
            MessageBox.Show(pesan, title == "" ? SB_TITLE_NOTIF : title, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        #endregion

        #region === Firebase Notification ===

        public void SendNotification(string deviceToken, string notifTitle, string notifBody, string dataValue1 = "", string dataValue2 = "")
        {
            if (string.IsNullOrEmpty(deviceToken))
            {
                return;
            }

            var notif = new
            {
                to = deviceToken,
                notification = new
                {
                    title = notifTitle,
                    body = notifBody,
                    click_action = "FLUTTER_NOTIFICATION_CLICK"
                },
                data = new
                {
                    value1 = dataValue1,
                    value2 = dataValue2
                },
                priority = "high"
            };
            var json = JsonConvert.SerializeObject(notif);

            // Let's make request
            var client = new RestClient();
            var request = new RestRequest(SB_FCM_URL, Method.Post);

            // Headers
            request.AddHeader("Authorization", "key=AAAA3lRBnFw:APA91bF9nrcSGjntQQ8eP7TOj0na126fIIBj3eVZqyurklCOQbvEGEnOIYX9QY2rPQp9LEUgIGyq7KpCG5SPXD4IAunngXcB11_-cwLn_CC26eKdwjCxSFBRRAk5hUVg4UiparIQKvuW");
            request.AddHeader("Content-Type", "application/json");

            // Payload
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            // Execute
            var result = client.ExecutePostAsync(request).Result;

            // Let's see
            var statusCode = (int)result.StatusCode;
            var debugInfo = $"StatusCode: {statusCode}, Content: {result.Content}";

            DebugHelper.ShowDebug("NotifHelper => SendNotification", debugInfo);

            //return response.Content; 
        }

        public void SendNotificationGroup(string topics, string notifTitle, string notifBody, string dataValue1 = "", string dataValue2 = "")
        {
            var notif = new
            {
                to = $"/topics/{topics}",
                notification = new
                {
                    title = notifTitle,
                    body = notifBody,
                    click_action = "FLUTTER_NOTIFICATION_CLICK"
                },
                data = new
                {
                    value1 = dataValue1,
                    value2 = dataValue2
                },
                priority = "high"
            };
            var json = JsonConvert.SerializeObject(notif);

            // Let's make request
            var client = new RestClient();
            var request = new RestRequest(SB_FCM_URL, Method.Post);

            // Headers
            request.AddHeader("Authorization", "key=AAAA3lRBnFw:APA91bF9nrcSGjntQQ8eP7TOj0na126fIIBj3eVZqyurklCOQbvEGEnOIYX9QY2rPQp9LEUgIGyq7KpCG5SPXD4IAunngXcB11_-cwLn_CC26eKdwjCxSFBRRAk5hUVg4UiparIQKvuW");
            request.AddHeader("Content-Type", "application/json");

            // Paylaod
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            // Execute
            var result = client.ExecutePostAsync(request).Result;

            // Let's see
            var statusCode = (int)result.StatusCode;
            var debugInfo = $"StatusCode: {statusCode}, Content: {result.Content}";

            DebugHelper.ShowDebug("NotifHelper => SendNotificationTopics", debugInfo);

        }

        #endregion
    }
}
