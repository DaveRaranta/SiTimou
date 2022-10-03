using System;
using System.IO;
using MimeTypes;
using RestSharp;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using gov.minahasa.sitimou.Helper;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace gov.minahasa.sitimou.RestApi
{
    internal class LaporanRest
    {
        #region === Constructor ===

        private readonly string _mainUrl;

        private readonly DatabaseHelper _db = new();
        private readonly NotifHelper _notifHelper = new();

        public LaporanRest()
        {
            var apiServer = _db.LoadApiInfo("SITIMOU")[0];

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

        public async Task<bool> SimpanProsesLaporan(string jenisLaporan, int idLaporan, string judul,
            string uraian, string status, string fileName, int idDisposisi)
        {
            try
            {
                var url = $"{_mainUrl}/lapor/proses_laporan/";
                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);

                client.Authenticator = new JwtAuthenticator(Globals.ApiToken);

                Console.WriteLine(url);

                request.RequestFormat = DataFormat.Json;

                // Header
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddHeader("cache-control", "no-cache");

                // Payload
                request.AddParameter("IdDisposisi", idDisposisi);
                request.AddParameter("IdOpd", Globals.UserOpdId!.Value);
                request.AddParameter("IdUser", Globals.UserId!.Value);
                request.AddParameter("JenisLaporan", jenisLaporan);
                request.AddParameter("IdLaporan", idLaporan);
                request.AddParameter("Judul", judul);
                request.AddParameter("Uraian", uraian);
                request.AddParameter("Status", status);
                request.AddFile("FileFoto", fileName, MimeTypeMap.GetMimeType(Path.GetExtension(fileName)));

                // Execute
                var result = await client.ExecutePostAsync(request);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var statusCode = result.StatusCode;
                    var numStatuscode = (int)statusCode;

                    throw new Exception($"HttpStatusCode: {numStatuscode}, Content: {result.Content}");
                }

                return true;


            }
            catch (Exception e)
            {
                DebugHelper.ShowError("LOKASI", @"LokasiRest", MethodBase.GetCurrentMethod()?.Name, e);
                return false;
            }
        }
        #endregion

        #region === Notifikasi ===

        public async void SendNotifikasi(int idUser, int idLaporan, string jenisLaporan)
        {
            var url = $"{_mainUrl}/info/send_notifikasi";
            var client = new RestClient();
            var request = new RestRequest(url, Method.Post);

            client.Authenticator = new JwtAuthenticator(Globals.ApiToken);

            var json = new
            {
                IdUser = idUser,
                IdLaporan = idLaporan,
                JenisLaporan = jenisLaporan,

            };

            try
            {
                request.AddJsonBody(json);


                var result = client.ExecutePostAsync(request).Result;

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var statusCode = result.StatusCode;
                    var numStatusCode = (int)statusCode;
                    throw new Exception($"StatusCode: {statusCode} / {numStatusCode}");
                }
            }
            catch (Exception e)
            {
                DebugHelper.ShowError("API TOKEN", @"AuthRest", MethodBase.GetCurrentMethod()?.Name, e);
            }
        }

        #endregion
    }

}
