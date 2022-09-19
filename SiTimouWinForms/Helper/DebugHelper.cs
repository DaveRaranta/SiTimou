using System;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace gov.minahasa.sitimou.Helper
{
    internal class DebugHelper : DbConnection
    {
        #region === Debug Info ===

        public static void ShowError(string appEvent, string src, string methode, Exception ex, string owntext = null, bool logError = false, bool logFullError = false)
        {

            Debug.WriteLine(" ");
            Debug.WriteLine(@"======= ERROR START =======");
            Debug.WriteLine(@"Date/Time:" + DateTime.Now.ToString("g"));
            Debug.WriteLine(owntext == null
                ? $"Error On: [{src}] at <{methode}> : {ex.Message}"
                : $"Error On: [{src}] at <{methode}> : {owntext}:::{ex.Message}");
            Debug.WriteLine($"Details: {ex}");
            Debug.WriteLine("======= ERROR END ======= ");
            Debug.WriteLine(" ");

            if (logError)
            {
                var x = new DebugHelper();
                var msg = logFullError ? ex.ToString() : ex.Message;

                x.SaveAppLog(appEvent, "ERROR", $"[{src}] at <{methode}> : {msg}");
            }
        }

        public static void ShowDebug(string src, string msg)
        {
#if DEBUG
            Debug.WriteLine($"[!] DebugInfo: [{src}] => {msg}");
#endif
        }

        public void SaveAppLog(string appEvent, string ketEvent, string detailEvent)
        {
            const string sql = "INSERT INTO log_app (tanggal, source, user_id, opd_id, event, ket_event, detail_event) " +
                               "VALUES (NOW(),'DESKTOP', @user, @opd, @event, @ket, @detail)";

            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@user", Globals.UserId);
                cmd.Parameters.AddWithValue("@opd", Globals.UserOpdId);
                cmd.Parameters.AddWithValue("@event", appEvent);
                cmd.Parameters.AddWithValue("@ket", ketEvent);
                cmd.Parameters.AddWithValue("@detail", detailEvent);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                DebugHelper.ShowError(@"SaveAppLog", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()!.Name, e);

            }
        }

        #endregion
    }
}
