using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using minahasa.sitimou.webapi.Helper;
using minahasa.sitimou.webapi.Models;
using MySqlConnector;
using Newtonsoft.Json;
using Exception = System.Exception;

namespace minahasa.sitimou.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AuthController : ControllerBase
    {
        private readonly CryptoHelper _crypto = new CryptoHelper();
        
        private readonly string _conDb;
        private readonly string _authToken;
        // private readonly string _baseFolder;

        public AuthController(IConfiguration config)
        {
            _conDb = config.GetConnectionString("MainDatabase");
            _authToken = config.GetSection("AppSettings:Token").Value;
            // _baseFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files");
        }

        #region === Login ===

        [AllowAnonymous]
        [HttpPost("masuk")]
        public async Task<IActionResult> MasukPenduduk()
        {
            try
            {
                MasukPenduduk? payload;
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var json = await reader.ReadToEndAsync();
                    payload = JsonConvert.DeserializeObject<MasukPenduduk>(json);
                }

                const string sql = @"SELECT user_id as IdUser, nik as Nik, pwd_hash as PwdHash, pwd_salt as PwdSalt, flg as Flg " + 
                                   "FROM pengguna WHERE nik = @p_nik";
                
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_nik", payload!.UserNik);
                
                // Open DB
                await conn.OpenAsync();
                var result = conn.QueryAsync<LoginResult>(sql, parms, commandType: CommandType.Text).Result.FirstOrDefault();

                if (result == null) return NotFound();
                
                // Cek Login dan Pwd
                if (_crypto.VerifyPassword(payload.UserPwd, result.PwdHash, result.PwdSalt))
                {
                    // Cek Status User
                    if (result.Flg != "N") return Unauthorized("USER_ACCESS_DENIED");
                    
                    // Buat JWT Token
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.IdUser.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authToken));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(150),
                        SigningCredentials = creds
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new { authToken = tokenHandler.WriteToken(token) });
                }
                else
                {
                    return Unauthorized("INVALID_PASSWORD");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
            
        }
        
        [AllowAnonymous]
        [HttpPost("masuk_pegawai")]
        public async Task<IActionResult> MasukPegawai()
        {
            MasukPegawai? payload;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var json = await reader.ReadToEndAsync();
                payload = JsonConvert.DeserializeObject<MasukPegawai>(json);
            }

            const string sql = @"SELECT user_id as IdUser, login as Login, pwd_hash as PwdHash, pwd_salt as PwdSalt, flg as Flg, grup as Grup " + 
                               "FROM pegawai WHERE login = @p_login";
            
            try
            {
                // Open DB
                await using var conn = new MySqlConnection(_conDb);
                
                var parms = new DynamicParameters();
                parms.Add("@p_login", payload!.UserLogin);
                
                await conn.OpenAsync();
                var result = conn.QueryAsync<LoginPegawaiResult>(sql, parms, commandType: CommandType.Text).Result.FirstOrDefault();

                if (result == null) return NotFound("USER_NOT_REGISTERD");
                
                if (_crypto.VerifyPassword(payload.UserPwd, result.PwdHash, result.PwdSalt))
                {
                    // Cek Status User
                    if (result.Flg != "N") return Unauthorized("USER_ACCESS_DENIED");
                    if (result.Grup != "4") return Unauthorized("USER_ACCESS_DENIED");
                    
                    // Token
                    // Buat claim
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.IdUser.ToString())
                    };

                    // buat key 
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authToken));

                    // buat sign creds
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    // buat security token dengan masa aktif 7 hari
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(90),
                        SigningCredentials = creds
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new { authToken = tokenHandler.WriteToken(token) });
                        
                }
                else
                {
                    return Unauthorized("USER_WRONG_LOGIN");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.ToString());
                
            }
            
        }
        #endregion

        #region === Daftar ===

        [AllowAnonymous]
        [HttpPost("daftar")]
        public async Task<IActionResult> Registrasi() 
        {
            try
            {
                RegistrasiPengguna? payload;
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var json = await reader.ReadToEndAsync();
                    payload = JsonConvert.DeserializeObject<RegistrasiPengguna>(json);
                }
                
                // Proses Password
                _crypto.CreatePassword(payload!.UserPwd, out var pwdHash, out var pwdSalt);
                
                // Simpan
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_nik", payload.UserNik);
                parms.Add("@p_nama", payload.UserNama);
                parms.Add("@p_pwd_hash", pwdHash);
                parms.Add("@p_pwd_salt", pwdSalt);

                await conn.OpenAsync();

                var result = Convert.ToInt16(conn.ExecuteScalarAsync("sp_registrasi_penduduk", parms,
                    commandType: CommandType.StoredProcedure).Result);

                return result switch
                {
                    0 => Unauthorized("SAVE_ERROR"),
                    1 => Unauthorized("NIK_EXIST"),
                    2 => Ok(),
                    _ => Unauthorized("SAVE_ERROR")
                };


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }
        
        #endregion

        #region === Lain-lain===

        [HttpGet("status_pengguna/{id:int}")]
        public async Task<IActionResult> StatusPengguna(int id)
        {
            const string sql = @"SELECT flg FROM pengguna WHERE user_id = @p_user_id";

            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_user_id", id);
                
                // Open DB
                await conn.OpenAsync();
                var result = conn.QueryAsync(sql, parms, commandType: CommandType.Text)
                    .Result.FirstOrDefault();

                return result == null ? NotFound() : Ok(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet("status_pegawai/{id:int}")]
        public async Task<IActionResult> StatusPegawai(int id)
        {
            const string sql = @"SELECT flg FROM pegawai WHERE user_id = @p_user_id";

            try
            {
                await using var conn = new MySqlConnection(_conDb);
                var parms = new DynamicParameters();
                parms.Add("@p_user_id", id);
                
                // Open DB
                await conn.OpenAsync();
                var result = conn.QueryAsync(sql, parms, commandType: CommandType.Text)
                    .Result.FirstOrDefault();

                return result == null ? NotFound() : Ok(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return StatusCode(500, e.ToString());
            }
        }

        [AllowAnonymous]
        [HttpPost("api_token")]
        public async Task<IActionResult> GetApiToken()
        {
            MasukPegawai? payload;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var json = await reader.ReadToEndAsync();
                payload = JsonConvert.DeserializeObject<MasukPegawai>(json);
            }

            const string sql = @"SELECT user_id as IdUser, login as Login, pwd_hash as PwdHash, pwd_salt as PwdSalt, flg as Flg " + 
                               "FROM pegawai WHERE login = @p_login";
            
            try
            {
                // Open DB
                await using var conn = new MySqlConnection(_conDb);
                
                var parms = new DynamicParameters();
                parms.Add("@p_login", payload!.UserLogin);
                
                await conn.OpenAsync();
                var result = conn.QueryAsync<LoginPegawaiResult>(sql, parms, commandType: CommandType.Text).Result.FirstOrDefault();

                if (result == null) return NotFound();
                
                if (_crypto.VerifyPassword(payload.UserPwd, result.PwdHash, result.PwdSalt))
                {
                    // Cek Status User
                    if (result.Flg != "N") return Unauthorized("USER_ACCESS_DENIED");
                    
                    // Token
                    // Buat claim
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.IdUser.ToString())
                    };

                    // buat key 
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authToken));

                    // buat sign creds
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    // buat security token dengan masa aktif 7 hari
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(1),
                        SigningCredentials = creds
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new { authToken = tokenHandler.WriteToken(token) });
                        
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.ToString());
                
            }
        }
        
        #endregion
    }    
}

