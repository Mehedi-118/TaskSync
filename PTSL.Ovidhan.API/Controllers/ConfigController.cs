using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PTSL.Ovidhan.Api.Controllers;

[ApiController]
public class ConfigController : Controller
{
    private readonly IConfiguration _configuration;

    public ConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("GetConfigs")]
    public IActionResult GetConfigs()
    {
        return Ok(new Config
        {
            ResourceUrl = _configuration.GetValue<string>("ResourceUrl") ?? string.Empty,
        });
    }
}

public class Config
{
    public string ResourceUrl { get; set; }
}

