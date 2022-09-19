using MySqlConnector;

namespace minahasa.sitimou.webapi.Helper
{
    public abstract class DbConnection
    {
        private readonly string primaryConnection;

        protected DbConnection()
        {
            // Ambil setings database
            var appSettings = AppSettings.GetAppSettings();
            primaryConnection = appSettings.GetConnectionString("MainDatabase");
        }

        protected MySqlConnection MainDbConnection()
        {
            return new MySqlConnection(primaryConnection);
        }
    }    
}

