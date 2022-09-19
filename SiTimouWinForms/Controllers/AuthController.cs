using System;
using System.Data;
using System.Reflection;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.RestApi;
using MySql.Data.MySqlClient;

namespace gov.minahasa.sitimou.Controllers
{
    internal class AuthController : DbConnection
    {
        #region === Constructor ===

        private readonly NotifHelper _notif = new();
        private readonly CryptoHelper _crypto = new();
        private readonly AuthRest _authRest = new();

        #endregion

        public bool Login(string nik, string password)
        {
            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand("sp_login", conn) { CommandType = CommandType.StoredProcedure };
            try
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@p_login", nik);

                using var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    _notif.MsgBoxWarning("NIK anda belum terdaftar. Silahkan hubungi admin untuk info selanjutnya.");
                    return false;
                }

                reader.Read();

                if (_crypto.VerifyPasswordHash(password, (byte[])reader[9], (byte[])reader[10]))
                {
                    if (reader[8].ToString() != "N")
                    {
                        _notif.MsgBoxWarning("Anda tidak diizinkan untuk menggunakan aplikasi ini. Hubungi admin untuk info selanjutnya.");
                        return false;
                    }

                    Globals.UserId = reader.GetInt16(0);
                    Globals.UserNip = reader.GetString(1);
                    Globals.UserNamaLengkap = reader.GetString(2);
                    Globals.UserGrup = reader.GetString(3);
                    Globals.UserJabatan = reader.GetString(4);
                    Globals.UserOpdSingkat = reader.GetString(5);
                    Globals.UserOpdLengkap = reader.GetString(6);
                    Globals.UserOpdId = reader.GetInt16(7);
                    Globals.UserPwd = password;

                    return true;

                }
                else
                {
                    _notif.MsgBoxWarning("Password yang anda masukan belum benar.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.ShowError("LOGIN", "AuthController", MethodBase.GetCurrentMethod()?.Name, ex);
                return false;
            }
        }

        public string GetApiToken(string login, string password)
        {
            var apiToken = _authRest.GetApiToken(login, password);

            return apiToken;

        }
    }
}
