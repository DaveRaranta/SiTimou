using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minahasa.sitimou.webapi.Helper;
using minahasa.sitimou.webapi.Models;
using MySqlConnector;

namespace minahasa.sitimou.webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class LokasiController : ControllerBase
    {
        private readonly string _conDb;
        private readonly string _baseFolder;
        
        private readonly DbHelper _dbHelper = new();
        private readonly ImageHelper _imageHelper = new();

        public LokasiController(IConfiguration config)
        {
            _conDb = config.GetConnectionString("MainDatabase");
            _baseFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files", "lokasi");

            // Create Folder
            if (!Directory.Exists(_baseFolder)) Directory.CreateDirectory(_baseFolder);
        }

        #region === Request ===

        [HttpGet("daftar_lokasi")]
        public async Task<IActionResult> DaftarLokasi()
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_lokasi_id", 0);
                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_list_lokasi", parms, commandType: CommandType.StoredProcedure)
                    .Result.ToList();

                return result.Count == 0 ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet("detail_lokasi/{id:int}")]
        public async Task<IActionResult> DetailLokasi(int id)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);
                
                var parms = new DynamicParameters();
                parms.Add("@p_lokasi_id", id);
                
                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_list_lokasi", parms, commandType: CommandType.StoredProcedure)
                    .Result.FirstOrDefault();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost("simpan_lokasi")]
        public async Task<IActionResult> SimpanLokasi([FromForm] SimpanLokasi payload)
        {
            var destFn = "";
            
            try            
            {
                // === Save data ke Db ===
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_jenis_lokasi", payload.JenisLokasi);
                parms.Add("@p_nama_lokasi", payload.NamaLokasi);
                parms.Add("@p_alamat", payload.Alamat);
                parms.Add("@p_desa_id", payload.IdDesa);
                parms.Add("@p_kecamatan_id", payload.IdKecamatan);
                parms.Add("@p_no_tlp", payload.NoTelp);
                parms.Add("@p_gps_lat", payload.GpsLat);
                parms.Add("@p_gps_lng", payload.GpsLng);
                parms.Add("@p_ket", payload.Keterangan);

                await conn.OpenAsync();
                var result = Convert.ToInt32(conn
                    .ExecuteScalarAsync("sp_save_lokasi", parms, commandType: CommandType.StoredProcedure)
                    .Result);

                if (result == 0) throw new Exception($"ERR_SAVE_LOKASI [{result}]");
                
                // Simpan file dengan nama ambil dari "result"
                destFn = Path.Combine(_baseFolder, $"{result}.jpg");
                
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

        [HttpGet("hapus_lokasi/{id:int}")]
        public async Task<IActionResult> HapusLokasi(int id)
        {
            try
            {
                // Hapus dari database
                var result = await _dbHelper.DeleteFromTable("lokasi_penting", "lokasi_id", id);
                if (!result) return StatusCode(500, "DELETE_DATA_FAILED");
            
                // Hapus file foto
                var fn = Path.Combine(_baseFolder, $"{id}.jpg");
                if (System.IO.File.Exists(fn)) System.IO.File.Delete(fn);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        #endregion

        #region === Foto Lokasi ===
        
        [HttpGet("foto_lokasi/{id:int}")]
        public Task<IActionResult> GetFotoLokasi(int id)
        {
            try
            {
                var srcFn = Path.Combine(_baseFolder, $"{id}.jpg");
                var defaultFn = Path.Combine(_baseFolder, "default.png");
                
                // CEk jika file ada
                var destFn = System.IO.File.Exists(srcFn) ? srcFn : defaultFn;
                
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
                var destFn = System.IO.File.Exists(srcFn) ? srcFn : defaultFn;
                
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
    }    
}

