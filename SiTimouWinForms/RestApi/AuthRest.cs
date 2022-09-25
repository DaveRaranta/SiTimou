using System;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Reflection;
using System.Diagnostics;
using gov.minahasa.sitimou.Helper;

namespace gov.minahasa.sitimou.RestApi
{
    internal class AuthRest
    {
        #region === Constructor ===

        private readonly string _mainUrl;

        private readonly DatabaseHelper _db = new();
        private readonly NotifHelper _notifHelper = new();

        public AuthRest()
        {
            var apiServer = _db.LoadApiInfo("SITIMOU")[0];

            if (apiServer == null || apiServer.Length <= 0)
            {
                _notifHelper.MsgBoxWarning(@"Gagal ambil alamat server. Cek koneksi database dan coba lagi.");
                return;
            }

            _mainUrl = $@"http://{apiServer}/api";

            // Set Api Url
            Globals.ApiAppBaseUrl = _mainUrl;

            Debug.WriteLine($@"API URL: {_mainUrl}");
        }

        #endregion

        #region === API ===
        
        public string GetApiToken(string login, string password)
        {
            var url = $"{_mainUrl}/auth/api_token";
            var client = new RestClient();
            var request = new RestRequest(url, Method.Post);
            var loginInfo = new
            {
                UserLogin = login,
                UserPwd = password
            };

            try
            {
                request.AddJsonBody(loginInfo);

                var result = client.ExecutePostAsync(request).Result;

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var statusCode = result.StatusCode;
                    var numStatusCode = (int)statusCode;
                    throw new Exception($"StatusCode: {statusCode} / {numStatusCode}");
                }

                dynamic payload = JsonConvert.DeserializeObject(result.Content ?? string.Empty);
                return payload?.authToken;
            }
            catch (Exception e)
            {
                _notifHelper.MsgBoxWarning("Gagal ambil Token. Hubungi admin untum memastikan web service aktif.");

                DebugHelper.ShowError("API TOKEN", @"AuthRest", MethodBase.GetCurrentMethod()?.Name, e);
                return string.Empty;
            }
        }

        #endregion

        

    }
}
