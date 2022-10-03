using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using minahasa.sitimou.webapi.Helper;
using minahasa.sitimou.webapi.Models;
using MySqlConnector;

namespace minahasa.sitimou.webapi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class InfoController : Controller
{
    
    private readonly string _conDb;
    private readonly string _baseFolder;
    
    private readonly DbHelper _dbHelper = new();

    public InfoController(IConfiguration config)
    {
        _conDb = config.GetConnectionString("MainDatabase");
        _baseFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files");
    }

    #region === Aturan ===
    
    [HttpGet("daftar_aturan")]
    public async Task<IActionResult> DaftarAturan()
    {
        try
        {
            await using var conn = new MySqlConnection(_conDb);
            
            var parms = new DynamicParameters();
            parms.Add("@p_jenis", "0");
            parms.Add("@p_opd_id", "0");
            
            await conn.OpenAsync();
            var result = conn.QueryAsync("sp_data_aturan", parms, commandType: CommandType.StoredProcedure)
                .Result.ToList();

            return result.Count == 0 ? NotFound() : Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return StatusCode(500, e.ToString());
        }
    }
    
    [HttpGet("detail_aturan/{id:int}")]
    public async Task<IActionResult> DetailAturan(int id)
    {
        try
        {
            await using var conn = new MySqlConnection(_conDb);

            var parms = new DynamicParameters();
            parms.Add("@p_aturan_id", id);

            await conn.OpenAsync();
            var result = conn.QueryAsync("sp_detail_aturan", parms, commandType: CommandType.StoredProcedure)
                .Result.FirstOrDefault();

            return result == null ? NotFound() : Ok(result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return StatusCode(500, e.ToString());
        }
    }

    #endregion

    #region === NOTIFIKASI ===

    [HttpPost("send_notifikasi")]
    public async Task<IActionResult> SendNotification(NotificationData payload)
    {
        try
        {
            var notifTitle = "";
            var notifBody = "";
            var fcmToken = _dbHelper.GetValueFromTable("pegawai", "fcm_token", "user_id", payload.IdUser);
            
            switch (payload.JenisLaporan)
            {
                case "1":
                {
                    // Ambil data Laporan
                    var pelaporId = _dbHelper.GetValueFromTable("laporan", "user_id", "laporan_id", payload.IdLaporan);
                    var namaPelapor = _dbHelper.GetValueFromTable("pengguna", "nama_lengkap", "user_id", pelaporId);
                    var tentang = _dbHelper.GetValueFromTable("laporan", "tentang", "laporan_id", payload.IdLaporan);

                    // Make Notif
                    notifTitle = "Laporan Masyarakat";
                    notifBody = $"a/n: {namaPelapor} mengenai '{tentang}'";
                    break;
                }
                case "2":
                {
                    var pelaporId = _dbHelper.GetValueFromTable("panik", "user_id", "laporan_id", payload.IdLaporan);
                    var namaPelapor = _dbHelper.GetValueFromTable("pengguna", "nama_lengkap", "user_id", pelaporId);

                    notifTitle = "PANIK";
                    notifBody = $"Laporan PANIK a/n: {namaPelapor}";
                    break;
                }
            }
        
            // Send notifikasi 
            FcmHelper.SendFcmNotification(fcmToken, notifTitle, notifBody);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.ToString());

        }
        
    }

    #endregion
        
}