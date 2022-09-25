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

namespace gov.minahasa.sitimou.RestApi
{
    internal class LokasiRest
    {
        #region === Constructor ===

        private readonly string _mainUrl;

        private readonly DatabaseHelper _db = new();
        private readonly NotifHelper _notifHelper = new();

        public LokasiRest()
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

        public async Task<bool> SimpanLokasiKhusus(string jenisLokasi, string namaLokasi, string alamat, int idDesa, int idKec, 
            string noTelp, double gpsLat, double gpsLng, string ket, string fileName)
        {
            try
            {
                var url = $"{_mainUrl}/lokasi/simpan_lokasi";
                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);

                client.Authenticator = new JwtAuthenticator(Globals.ApiToken);

                request.RequestFormat = DataFormat.Json;

                // Header
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddHeader("cache-control", "no-cache");

                // Payload
                request.AddParameter("JenisLokasi", jenisLokasi);
                request.AddParameter("NamaLokasi", namaLokasi);
                request.AddParameter("Alamat", alamat);
                request.AddParameter("IdDesa", idDesa);
                request.AddParameter("IdKecamatan", idKec);
                request.AddParameter("NoTelp", noTelp);
                request.AddParameter("GpsLat", gpsLat);
                request.AddParameter("GpsLng", gpsLng);
                request.AddParameter("Keterangan", ket);
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

        public async Task<bool> HapusLokasiKhusus(int idLokasi)
        {
            try
            {
                var url = $"{_mainUrl}/lokasi/hapus_lokasi/{idLokasi}";
                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);

                Console.WriteLine(url);

                client.Authenticator = new JwtAuthenticator(Globals.ApiToken);

                var result = await client.ExecuteGetAsync(request);

                if (result.StatusCode == HttpStatusCode.OK) return true;

                var statusCode = result.StatusCode;
                var numStatuscode = (int)statusCode;

                if (result.Content!.Contains("FILE_NOT_EXSIST")) _notifHelper.MsgBoxWarning("File Surat Keluar tidak ada.");

                throw new Exception($"HttpStatusCode: {numStatuscode}, Content: {result.Content}");


            }
            catch (Exception e)
            {
                DebugHelper.ShowError("LOKASI", @"LokasiRest", MethodBase.GetCurrentMethod()?.Name, e);
                return false;
            }
        }

        public async Task<byte[]> DownloadFotoLokasi(int idLokasi)
        {
            try
            {
                var url = $"{_mainUrl}/lokasi/foto_lokasi/{idLokasi}";
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
