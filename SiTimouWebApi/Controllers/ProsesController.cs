using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using minahasa.sitimou.webapi.Helper;
using minahasa.sitimou.webapi.Models;

namespace minahasa.sitimou.webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProsesController : ControllerBase
    {
        private readonly string _conDb;
        private readonly string _baseFolder;

        private readonly DbHelper _dbHelper = new();
        private readonly ImageHelper _imageHelper = new();

        public ProsesController(IConfiguration config)
        {
            _conDb = config.GetConnectionString("MainDatabase");
            _baseFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files", "proses");

            // Create Folder
            if (!Directory.Exists(_baseFolder)) Directory.CreateDirectory(_baseFolder);
        }

        #region === Laporan ===

        

        #endregion
        
        #region === Panik ===

        

        #endregion

    }
    
}

