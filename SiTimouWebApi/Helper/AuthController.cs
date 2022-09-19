using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace minahasa.sitimou.webapi.Helper
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly string _conDb;
        private readonly string _baseFolder;

        public AuthController(IConfiguration config)
        {
            _conDb = config.GetConnectionString("MainDatabase");
            _baseFolder = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}files");
        }
    
    
    }
}

