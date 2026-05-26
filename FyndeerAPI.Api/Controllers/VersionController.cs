using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FyndeerAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VersionController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public VersionController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    /// <summary>
    /// Returns API name, version, environment, and current server time.
    /// </summary>
    [HttpGet]
    public IActionResult GetVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";

        return Ok(new
        {
            name = "Fyndeer API",
            version,
            environment = _environment.EnvironmentName,
            serverTime = DateTimeOffset.UtcNow
        });
    }

    /// <summary>
    /// Simple connectivity test — returns pong with a timestamp.
    /// </summary>
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok(new { message = "pong", timestamp = DateTimeOffset.UtcNow });
    }

    /// <summary>
    /// Echoes back the provided message — useful for verifying request/response round-trips.
    /// </summary>
    [HttpGet("echo")]
    public IActionResult Echo([FromQuery] string message)
    {
        return Ok(new { echo = message, timestamp = DateTimeOffset.UtcNow });
    }
}
