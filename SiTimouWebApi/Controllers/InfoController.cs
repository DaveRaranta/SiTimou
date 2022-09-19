using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok("INFO");
    }

}