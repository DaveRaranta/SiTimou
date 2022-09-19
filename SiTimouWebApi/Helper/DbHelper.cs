
using MySqlConnector;
using System.Data;
using System.Diagnostics;

namespace minahasa.sitimou.webapi.Helper
{
    public class DbHelper : DbConnection
    {
        #region === Get Data ===

        public dynamic GetValueFromTable(string table, string selectField, string searchField, dynamic searchValue)
        {
            var sql = $"SELECT {selectField} FROM {table} WHERE {searchField} = @value ";

            using var conn = MainDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@value", searchValue);

                using var reader = cmd.ExecuteReader();
                if (!reader.HasRows) return null;

                reader.Read();

                return !DBNull.Value.Equals(reader[selectField]) ? reader[selectField] : null;

            }
            catch (Exception ex)
            {
                Console.WriteLine($@"[ERROR] Database => GetValueFromTable: {ex}");
                return null;
            }
        }

        public dynamic GetValueFromTableSql(string sql)
        {
            using var conn = MainDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                return cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Console.WriteLine($@"[ERROR] Database => GetValueFromTable: {ex}");
                return null;
            }
        }

        public bool ExecuteSqlNonTrans(string sqlString)
        {
            using var conn = MainDbConnection();
            using var cmd = new MySqlCommand(sqlString, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();
                
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                // Helper.ShowError("Database", MethodBase.GetCurrentMethod().Name, ex);

                return false;
            }
        }
        
        #endregion

        #region === Proses Data ===

        public async Task<bool> DeleteFromTable(string table, string searchField, dynamic searchValue)
        {
            // var sql = searchValue is string
            //     ? $"DELETE FROM {table} WHERE {searchField} = '{searchValue}'"
            //     : $"DELETE FROM {table} WHERE {searchField} = {searchValue}";

            var sql = $"DELETE FROM {table} WHERE {searchField} = @value";

            await using var conn = MainDbConnection();
            await using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@value", searchValue);

                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@$"[ERROR] Database => DeleteFromTable: {ex}");
                return false;
            }
        }

        public async Task<bool> DeleteFromTableSp(string spName, string prmName, object value)
        {
            // var sql = searchValue is string
            //     ? $"DELETE FROM {table} WHERE {searchField} = '{searchValue}'"
            //     : $"DELETE FROM {table} WHERE {searchField} = {searchValue}";

            await using var conn = MainDbConnection();
            await using var cmd = new MySqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure };
            try
            {
                conn.Open();

                cmd.Parameters.AddWithValue($"@{prmName}", value);

                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@$"[ERROR] Database => DeleteFromTableSp: {ex}");
                return false;
            }
        }


        #endregion
    }
}
