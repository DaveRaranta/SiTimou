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
    
    public class HomeController : ControllerBase
    {
        private readonly string _conDb;
        private readonly string _basePenggunaFolder;
        private readonly string _basePegawaiFolder;

        private readonly DbHelper _dbHelper = new();
        private readonly ImageHelper _imageHelper = new();

        public HomeController(IConfiguration config)
        {
            _conDb = config.GetConnectionString("MainDatabase");
            _basePenggunaFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files", "profil", "pengguna");
            _basePegawaiFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files", "profil", "pegawai");
            
            // Create Folder
            if (!Directory.Exists(_basePenggunaFolder)) Directory.CreateDirectory(_basePenggunaFolder);
            if (!Directory.Exists(_basePegawaiFolder)) Directory.CreateDirectory(_basePegawaiFolder);
        }

        #region === Info ===

        [HttpGet("info_pengumuman")]
        public async Task<IActionResult> InfoPengumuman()
        {
            const string sql = "SELECT TBTXT1 FROM mstbl WHERE TBCOD = 'APP' AND TBKEY = 'HOME' AND TBTXT0 = 'INFO'";

            var result = _dbHelper.GetValueFromTableSql(sql);

            return result == null ? Ok("-") : Ok(result);
        }

        [HttpGet("info_pengguna/{id:int}")]
        public async Task<IActionResult> InfoPengguna(int id)
        {
            try
            {
                await using var conn = new MySqlConnection(_conDb);

                var parms = new DynamicParameters();
                parms.Add("@p_jenis_user", "1");
                parms.Add("@p_user_id", id);
                
                await conn.OpenAsync();
                var result = conn.QueryAsync("sp_info_users", parms, commandType: CommandType.StoredProcedure)
                    .Result.FirstOrDefault();

                return result == null ? NotFound() : Ok(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }

        [HttpGet("daftar_kecamatan")]
        public async Task<IActionResult> Daftarkecamatan()
        {
            try
            {
                const string sql = $"SELECT kecamatan_id, nama_kecamatan FROM mskecamatan WHERE flg = 'N' ORDER BY nama_kecamatan";
                await using var conn = new MySqlConnection(_conDb);
                await conn.OpenAsync();
                var result = conn.QueryAsync(sql, commandType: CommandType.Text)
                    .Result.ToList();

                return result.Count == 0 ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet("daftar_desa/{id:int}")]
        public async Task<IActionResult> DaftarDesa(int id)
        {
            try
            {
                const string sql = $"SELECT desa_id, nama_desa FROM msdesa WHERE kecamatan_id = @p_id_kecamatan AND flg = 'N' ORDER BY nama_desa";
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_id_kecamatan", id);
                
                await conn.OpenAsync();
                var result = conn.QueryAsync(sql, parms, commandType: CommandType.Text)
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

        #region === Profil ===

        [HttpGet("foto_profil_pengguna/{id:int}")]
        public Task<IActionResult> GetFotoProfil(int id)
        {
            try
            {
                var srcFn = Path.Combine(_basePenggunaFolder, $"{id}.jpg");
                var defaultFn = Path.Combine(_basePenggunaFolder, "default.png");
                
                // CEk jika file ada
                var destFn = System.IO.File.Exists(srcFn) ? srcFn : defaultFn;
                
                var byteImg = _imageHelper.ImageToByte(_imageHelper.ResizeImage(Image.FromFile(destFn), 75), ImageFormat.Jpeg) ;
                
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

        [HttpPost("update_foto_pengguna")]
        public async Task<IActionResult> UpdateFotoPengguna([FromForm] UpdateFotoPengguna payload)
        {
            try
            {
                var destFn = Path.Combine(_basePenggunaFolder, $"{payload.IdUser}.jpg");
                await using (var stream = new FileStream(destFn, FileMode.Create, FileAccess.Write))
                {
                    await payload.FileFoto.CopyToAsync(stream);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost("update_profil")]
        public async Task<IActionResult> UpdateProfil(UpdateProfil payload)
        {
            try
            {
                // Simpan
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_user_id", payload.IdUser);
                parms.Add("@p_nik", payload.Nik);
                parms.Add("@p_nama_lengkap", payload.NamaLengkap);
                parms.Add("@p_tanggal_lahir", payload.TanggalLahir);
                parms.Add("@p_jenis_kelamin", payload.JenisKelamin);
                parms.Add("@p_alamat", payload.Alamat);
                parms.Add("@p_desa_id", payload.IdDesa);
                parms.Add("@p_kecamatan_id", payload.IdKecamatan);
                parms.Add("@p_no_tlp", payload.NoTelp);

                await conn.OpenAsync();
                var result = conn.ExecuteAsync("sp_update_profil", parms, commandType: CommandType.StoredProcedure)
                    .Result;

                if (result == 0) throw new Exception($"ERR_UPDATE_PROFIL [{result}]");

                return Ok();
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

