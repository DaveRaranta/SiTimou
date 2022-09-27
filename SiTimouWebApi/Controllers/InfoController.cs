using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace minahasa.sitimou.webapi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class InfoController : Controller
{
    private readonly string _conDb;
    private readonly string _baseFolder;

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
    
        
}