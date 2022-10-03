using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using minahasa.sitimou.webapi.Helper;
using minahasa.sitimou.webapi.Models;

namespace minahasa.sitimou.webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class ProsesController : ControllerBase
    {
        private readonly string _conDb;
        private readonly string _baseFolder;

        private readonly DbHelper _dbHelper = new();
        private readonly ImageHelper _imageHelper = new();

        public ProsesController(IConfiguration config)
        {
            _conDb = config.GetConnectionString("MainDatabase");
            _baseFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files", "proses");

            // Create Folder
            if (!Directory.Exists(_baseFolder)) Directory.CreateDirectory(_baseFolder);
        }

        #region === Laporan ===

        [HttpGet("daftar_laporan_masuk/{id:int}")]
        public async Task<IActionResult> DaftarLaporanMasuk(int id)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_user_id", id);

                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_data_disposisi_fr", parms, commandType: CommandType.StoredProcedure)
                    .Result.ToList();

                return result.Count == 0 ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost("laporan")]
        public async Task<IActionResult> SimpanProsesLaporan([FromForm] ProsesLaporan payload)
        {
            var destFn = "";

            try
            {
                // Ambil OPD user
                var opdId = _dbHelper.GetValueFromTable("pegawai", "opd_id", "user_id", payload.IdUser);
                if (opdId == null) return NotFound("UNIT_NOT_FOUND");
                
                // === Save data ke Db ===
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_disposisi_id", payload.IdDisposisi);
                parms.Add("@p_opd_id", opdId);
                parms.Add("@p_user_id", payload.IdUser);
                parms.Add("@p_jenis_laporan", payload.JenisLaporan);
                parms.Add("@p_laporan_id", payload.IdLaporan);
                parms.Add("@p_judul", payload.Judul);
                parms.Add("@p_uraian", payload.Uraian);
                parms.Add("@p_status", payload.Status);

                await conn.OpenAsync();
                var result = conn
                    .ExecuteAsync("sp_save_proses_laporan", parms, commandType: CommandType.StoredProcedure)
                    .Result;

                if (result == 0) throw new Exception($"ERR_SAVE_PROSES [{result}]");

                // Simpan file dengan nama ambil dari "result"
                var jenis = payload.JenisLaporan == "1" ? "laporan" : "panik";
                var subFolder = Path.Combine(_baseFolder, jenis);

                if (!Directory.Exists(subFolder)) Directory.CreateDirectory(subFolder);

                destFn = Path.Combine(subFolder, $"{payload.IdLaporan}.jpg");

                await using (var stream = new FileStream(destFn, FileMode.Create, FileAccess.Write))
                {
                    await payload.FileFoto.CopyToAsync(stream);
                }

                return Ok();
            }

            catch (Exception e)
            {
                if (System.IO.File.Exists(destFn)) System.IO.File.Delete(destFn);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("detail_laporan")]
        public async Task<IActionResult> DetailPanik(DetailLaporanFr payload)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);

                var parms = new DynamicParameters();
                parms.Add("@p_jenis_laporan", payload.JenisLaporan);
                parms.Add("@p_laporan_id", payload.IdLaporan);

                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_detail_laporan_fr", parms, commandType: CommandType.StoredProcedure)
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

        #region === Proses ===

        [HttpGet("terima_laporan/{id:int}")]
        public async Task<IActionResult> TerimaLaporan(int id)
        {
            const string sql = $"UPDATE disposisi_er SET flg = 'P' WHERE disposisi_id = @p_disposisi_id";

            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_disposisi_id", id);

                await conn.OpenAsync();
                var result = conn.ExecuteAsync(sql, parms, commandType: CommandType.Text)
                    .Result;

                if (result == 0) throw new Exception($"ERR_SAVE_TERIMA [{result}]");

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("batal_laporan")]
        public async Task<IActionResult> BatalLaporan(BatalLaporan payload)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_posisi", "3");
                parms.Add("@p_data_id", payload.IdLaporan);
                parms.Add("@p_user_id", payload.IdUser);

                await conn.OpenAsync();
                var result = conn.ExecuteAsync("sp_batal_laporan", parms, commandType: CommandType.StoredProcedure)
                    .Result;

                if (result == 0) throw new Exception($"ERR_BATAL_LAPORAN [{result}]");

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }




        #endregion

        #region === Riwayat ===

        [HttpPost("riwayat_laporan")]
        public async Task<IActionResult> DaftarRiwayatProses(RiwayatProsesLaporan payload)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_jenis_data", payload.JenisData);
                parms.Add("@p_data_id", payload.IdUser);

                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_data_riwayat_laporan", parms, commandType: CommandType.StoredProcedure)
                    .Result.ToList();

                return result.Count == 0 ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }


        #endregion

    }
    
}

