using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gov.minahasa.sitimou.Helper
{
    internal class DatabaseHelper : DbConnection
    {
        #region === Master Table Data ===

        public string[] LoadApiInfo(string urlType)
        {
            var sql = $"SELECT TBTXT1, TBNUM0 FROM mstbl WHERE TBCOD = 'APP' AND TBKEY = 'APIURL' AND TBTXT0 = '{urlType}'";

            Console.WriteLine("SQL: " + sql);

            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                using var reader = cmd.ExecuteReader();
                if (!reader.HasRows) return null;

                reader.Read();

                var arr = new[]
                {
                    reader[0].ToString(),
                    reader[1].ToString()
                };
                return arr;
            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);
                return null;
            }
        }

        #endregion

        #region === Check Data ===

        public bool CheckDataExist(string selectCol, string fromTable, string whereCol, dynamic checkValue)
        {
            var sql = checkValue is string
                ? $"SELECT {selectCol} FROM {fromTable} WHERE {whereCol} = '{checkValue}'"
                : $"SELECT {selectCol} FROM {fromTable} WHERE {whereCol} =  {checkValue} ";

            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();
                using var reader = cmd.ExecuteReader();
                return reader.HasRows;

            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);
                return false;
            }
        }

        public bool CheckDataExistSql(string sql)
        {
            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();
                using var reader = cmd.ExecuteReader();
                return reader.HasRows;

            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);
                return false;
            }
        }

        #endregion

        #region === Operasi Data ===

        public dynamic GetDataFromTable(string selectCol, string fromTable, string whereCol, dynamic checkValue)
        {
            var sql = checkValue is string
                ? $"SELECT {selectCol} FROM {fromTable} WHERE {whereCol} = '{checkValue}'"
                : $"SELECT {selectCol} FROM {fromTable} WHERE {whereCol} =  {checkValue} ";

            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                return cmd.ExecuteScalar();

                // using var reader = cmd.ExecuteReader();

                //if (!reader.HasRows) return null;

                // reader.Read();
                // return reader[0];

            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);
                return null;
            }

        }

        public dynamic GetDataFromTableSql(string sql)
        {

            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                return cmd.ExecuteScalar();

            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);
                return null;
            }

        }

        public dynamic UpdateDataToTable(string table, string column, dynamic value, string conditionColumn, dynamic conditionValue, bool returnResult = false)
        {
            // UPDATE {table} SET {col} = value
            var sql = "";

            if (value is string && conditionValue is int) sql = @$"UPDATE {table} SET {column} = '{value}' WHERE {conditionColumn} = {conditionValue}";
            if (value is string && conditionValue is string) sql = @$"UPDATE {table} SET {column} = '{value}' WHERE {conditionColumn} = '{conditionValue}'";
            if (value is int && conditionValue is int) sql = @$"UPDATE {table} SET {column} = {value} WHERE {conditionColumn} = {conditionValue}";
            if (value is int && conditionValue is string) sql = @$"UPDATE {table} SET {column} = {value} WHERE {conditionColumn} = '{conditionValue}'";

            DebugHelper.ShowDebug("[DatabaseHelper] UpdateDataToTable", $"SQL: {sql}");

            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();
                var result = cmd.ExecuteNonQuery();

                DebugHelper.ShowDebug("[DatabaseHelper] UpdateDataToTable", $"Rows Efected: {result}");

                if (returnResult) return result;

                return true;
            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);
                return false;
            }

        }

        public dynamic UpdateDataToTableSql(string sql, bool returnResult = false)
        {
            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();
                var result = cmd.ExecuteNonQuery();

                DebugHelper.ShowDebug("[DatabaseHelper] UpdateDataToTable", $"Rows Efected: {result}");

                if (returnResult) return result;

                return true;
            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);
                return false;
            }

        }

        public bool ExecuteSqlString(string sqlString)
        {
            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sqlString, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);

                return false;
            }
        }

        public bool ExecuteSqlStringWithParm(string sqlString, string parm, dynamic value)
        {
            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sqlString, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                cmd.Parameters.AddWithValue($"@{parm}", value);

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);

                return false;
            }
        }

        public object ExecuteScalarSql(string sql)
        {
            using var conn = GetDbConnection();
            using var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text };
            try
            {
                conn.Open();

                return cmd.ExecuteScalar();


            }
            catch (Exception e)
            {
                DebugHelper.ShowError("DATABASE", @"[DatabaseHelper]", MethodBase.GetCurrentMethod()?.Name, e);

                return null;
            }
        }

        #endregion
    }
}
