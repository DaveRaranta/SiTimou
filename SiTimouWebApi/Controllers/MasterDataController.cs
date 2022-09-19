using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace minahasa.sitimou.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MasterDataController : Controller
    {
        private readonly string _conDb;
        // private readonly string _baseFolder;

        public MasterDataController(IConfiguration config)
        {
            _conDb = config.GetConnectionString("MainDatabase");
            // _baseFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files");
        }
        
        [HttpGet("kecamatan")]
        public async Task<IActionResult> DaftarKecamatan()
        {
            const string sql = @"SELECT kecamatan_id, nama_kecamatan FROM mskecamatan ORDER BY nama_kecamatan";

            try
            {
                await using var conn = new MySqlConnection(_conDb);
                await conn.OpenAsync();
                var result = conn.QueryAsync(sql, commandType: CommandType.Text)
                    .Result.ToList();

                return result.Count == 0 ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.ToString());
            }
        }

        [HttpGet("desa/{id:int}")]
        public async Task<IActionResult> DaftarDesa(int id)
        {
            const string sql = @"SELECT desa_id, nama_desa FROM msdesa WHERE kecamatan_id = @p_kec_id ORDER BY nama_desa";

            try
            {
                await using var conn = new MySqlConnection(_conDb);

                var parms = new DynamicParameters();
                parms.Add("@p_kec_id", id);
                
                await conn.OpenAsync();
                var result = conn.QueryAsync(sql, parms, commandType: CommandType.Text)
                    .Result.ToList();

                return result.Count == 0 ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.ToString());
            }
        }

    }    
}



