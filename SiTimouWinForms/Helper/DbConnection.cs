using System.Diagnostics;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace gov.minahasa.sitimou.Helper
{
    public abstract class DbConnection
    {
        private readonly string _connectionString;

        protected DbConnection()
        {
            var registry = new RegistryCore()
            {
                SubKey = "SOFTWARE\\Minahasa\\" + Application.ProductName + "\\db"
            };

            var dbServer = CryptoHelper.DecryptString((string)registry.Read("db_server"));
            var dbPort = CryptoHelper.DecryptString((string)registry.Read("db_port")); // 8731
            var dbCatalog = CryptoHelper.DecryptString((string)registry.Read("db_catalog"));
            var dbUser = CryptoHelper.DecryptString((string)registry.Read("db_user"));
            var dbPassword = CryptoHelper.DecryptString((string)registry.Read("db_pwd")); // h7GXZcdNdjd4b8mV
            const int dbTimeout = 600; //(int)registry.Read("ConnectionTimeout");

            // var conString = $"server={_dbServer}; port={_dbPort}; user id={_dbUser}; password={_dbPassword}; database={_dbCatalog}; CharSet=utf8mb4; Connect Timeout={_dbTimeout}";
            _connectionString = $"server={dbServer}; port={dbPort}; database={dbCatalog}; uid={dbUser}; pwd={dbPassword}; CharSet=utf8mb4; Connect Timeout={dbTimeout}";

            Debug.WriteLine(_connectionString);
        }

        protected MySqlConnection GetDbConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
