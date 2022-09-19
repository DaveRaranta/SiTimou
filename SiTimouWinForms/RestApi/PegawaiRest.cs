using gov.minahasa.sitimou.Helper;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gov.minahasa.sitimou.RestApi
{
    internal class PegawaiRest
    {
        #region === Constructor ===

        private readonly string _mainUrl;

        private readonly DatabaseHelper _db = new();
        private readonly NotifHelper _notifHelper = new();

        public PegawaiRest()
        {
            var apiServer = _db.LoadApiInfo("MITRAFR")[0];

            if (apiServer is not { Length: > 0 })
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

        #region === Request ===

        public async Task<byte[]> DownloadFotoProfil(int idPengguna)
        {
            try
            {
                var url = $"{_mainUrl}/home/foto_profil_pengguna/{Globals.UserId}";
                var client = new RestClient();
                var request = new RestRequest(url, Method.Get);

                client.Authenticator = new JwtAuthenticator(Globals.ApiToken);

                var result = await client.ExecuteGetAsync(request);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var statusCode = result.StatusCode;
                    var numStatuscode = (int)statusCode;

                    // if (result.Content!.Contains("FILE_NOT_EXSIST")) _notifHelper.MsgBoxWarning("File Surat Keluar tidak ada.");

                    throw new Exception($"HttpStatusCode: {numStatuscode}, Content: {result.Content}");
                }

                var byteFile = await client.DownloadDataAsync(request);

                return byteFile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        #endregion
    }
}
