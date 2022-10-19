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
    
    public class LaporController : ControllerBase
    {
        private readonly string _conDb;
        private readonly string _baseFolder;
        private readonly string _baseProsesFolder;

        private readonly DbHelper _dbHelper = new();
        private readonly ImageHelper _imageHelper = new();

        public LaporController(IConfiguration config)
        {
            _conDb = config.GetConnectionString("MainDatabase");
            _baseFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files", "laporan");
            _baseProsesFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files", "proses");

            // Create Folder
            if (!Directory.Exists(_baseFolder)) Directory.CreateDirectory(_baseFolder);
        }

        #region  === My Laporan ===

        [HttpGet("daftar_laporan_id/{id:int}")]
        public async Task<IActionResult> DaftarLaporanId(int id)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_user_id", id);
                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_laporan_by_user", parms, commandType: CommandType.StoredProcedure)
                    .Result.ToList();

                return result.Count == 0 ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet("daftar_laporan_all")]
        public async Task<IActionResult> DaftarLaporanAll()
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);
                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_laporan_all", commandType: CommandType.StoredProcedure)
                    .Result.ToList();

                return result.Count == 0 ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet("detail_laporan/{id:int}")]
        public async Task<IActionResult> DetailLaporan(int id)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);

                var parms = new DynamicParameters();
                parms.Add("@p_laporan_id", id);
                parms.Add("@p_jenis_data", "2");
                
                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_detail_laporan", parms, commandType: CommandType.StoredProcedure)
                    .Result.FirstOrDefault();

                return result == null ? NotFound() : Ok(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }

        [HttpGet("hapus_laporan/{id:int}")]
        public async Task<IActionResult> HapusLaporan(int id)
        {
            try
            {
                // Ambil 'user_id'
                var userId = _dbHelper.GetValueFromTable("laporan", "user_id", "laporan_id", id);
                var userNik = _dbHelper.GetValueFromTable("pengguna", "nik", "user_id", userId);
            
                // Hapus dari database
                var result = await _dbHelper.DeleteFromTable("laporan", "laporan_id", id);
                if (!result) return StatusCode(500, "DELETE_DATA_FAILED");
            
                // Hapus file foto
                var fn = Path.Combine(_baseFolder, userNik, $"{id}.jpg");
                if (System.IO.File.Exists(fn)) System.IO.File.Delete(fn);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        [HttpPost("simpan_laporan")]
        public async Task<IActionResult> SimpanLaporan([FromForm] SimpanLaporan payload)
        {
            var destFn = "";
            
            try            
            {
                // === Save data ke Db ===
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_user_id", payload.IdUser);
                parms.Add("@p_perihal", payload.Perihal);
                parms.Add("@p_isi_laporan", payload.IsiLaporan);
                parms.Add("@p_gps_lat", payload.GpsLat);
                parms.Add("@p_gps_lng", payload.GpsLng);

                await conn.OpenAsync();
                var result = Convert.ToInt32(conn
                    .ExecuteScalarAsync("sp_save_laporan", parms, commandType: CommandType.StoredProcedure)
                    .Result);

                if (result == 0) throw new Exception($"ERR_SAVE_LOKASI [{result}]");
                
                // Simpan file dengan nama ambil dari "result"
                var userId = _dbHelper.GetValueFromTable("laporan", "user_id", "laporan_id", result);
                var userNik = _dbHelper.GetValueFromTable("pengguna", "nik", "user_id", userId);
                var subFolder = Path.Combine(_baseFolder, userNik);
                
                if (!Directory.Exists(subFolder)) Directory.CreateDirectory(subFolder);

                destFn = Path.Combine(subFolder, $"{result}.jpg");

                await using (var stream = new FileStream(destFn, FileMode.Create, FileAccess.Write))
                {
                    await payload.FileFoto.CopyToAsync(stream);
                }

                return Ok();
            }

            catch (Exception e)
            {
                if(System.IO.File.Exists(destFn)) System.IO.File.Delete(destFn);
                return StatusCode(500, e.Message);
            }
            
        }

        #endregion

        #region === Panic ===

        [HttpPost("panik")]
        public async Task<IActionResult> SimpanPanik(SimpanPanik payload)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_user_id", payload.IdUser);
                parms.Add("@p_gps_lat", payload.GpsLat);
                parms.Add("@p_gps_lng", payload.GpsLng);
                
                await conn.OpenAsync();
                var result = conn.ExecuteAsync("sp_save_panik", parms, commandType: CommandType.StoredProcedure)
                    .Result;

                if (result == 0) throw new Exception($"ERR_SAVE_PANIK [{result}]");

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        

        #endregion
        
        #region === Foto Laporan ===
        
        [HttpGet("foto_lokasi/{id:int}")]
        public Task<IActionResult> GetFotoLokasi(int id)
        {
            try
            {
                // Ambil 'user_id'
                var userId = _dbHelper.GetValueFromTable("laporan", "user_id", "laporan_id", id);
                var userNik = _dbHelper.GetValueFromTable("pengguna", "nik", "user_id", userId);

                // File 
                var srcFn = "";
                var defaultFn = Path.Combine(_baseFolder, "default.png");
                
                srcFn = userNik == null 
                    ? defaultFn
                    : (string)Path.Combine(_baseFolder, userNik, $"{id}.jpg");

                // CEk jika file ada
                var byteImg = System.IO.File.Exists(srcFn) 
                    ? _imageHelper.ImageToByte(Image.FromFile(srcFn), ImageFormat.Jpeg) 
                    : _imageHelper.ImageToByte(Image.FromFile(defaultFn),ImageFormat.Png);
                
                return Task.FromResult<IActionResult>(File(byteImg, "application/octet-stream"));

                //var stream = System.IO.File.OpenRead(destFn);
                //if (stream == null) return NotFound();
                
                //return File(stream, "application/octet-stream");
                

            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(StatusCode(500, ex.ToString()));
            }
        }

        [HttpGet("foto_tmb_lokasi/{id:int}")]
        public Task<IActionResult> GetFotoThumbnailLokasi(int id)
        {
            try
            {
                var srcFn = Path.Combine(_baseFolder, $"{id}.jpg");
                var defaultFn = Path.Combine(_baseFolder, "default.png");
                
                // CEk jika file ada
                //var destFn = System.IO.File.Exists(srcFn) ? srcFn : defaultFn;
                
                var byteImg = System.IO.File.Exists(srcFn) 
                    ? _imageHelper.ImageToByte(_imageHelper.ResizeImage(Image.FromFile(srcFn), 75), ImageFormat.Jpeg) 
                    : _imageHelper.ImageToByte(_imageHelper.ResizeImage(Image.FromFile(defaultFn), 50), ImageFormat.Jpeg);
                
                return Task.FromResult<IActionResult>(File(byteImg, "application/octet-stream"));

                //var stream = System.IO.File.OpenRead(destFn);
                //if (stream == null) return NotFound();
                
                //return File(stream, "application/octet-stream");
                

            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(StatusCode(500, ex.ToString()));
            }
        }
        #endregion

        #region === Proses Laporan ===

        [HttpPost("proses_laporan")]
        public async Task<IActionResult> SimpanProsesLaporan([FromForm] SimpanProsesLaporan payload)
        {
            var destFn = "";
            
            try            
            {
                // === Save data ke Db ===
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_disposisi_id", payload.IdDisposisi);
                parms.Add("@p_opd_id", payload.IdOpd);
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
                var subFolder = Path.Combine(_baseProsesFolder, jenis);

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
                if(System.IO.File.Exists(destFn)) System.IO.File.Delete(destFn);
                return StatusCode(500, e.Message);
            }
        }

        #endregion
        
    }    
    
}

