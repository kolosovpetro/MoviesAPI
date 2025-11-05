using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Core.Controllers;

[ApiController]
public class HealthController : Controller
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("health/live")]
    [SwaggerOperation(Summary = "Liveness probe.")]
    public async Task<IActionResult> Alive()
    {
        _logger.LogInformation("Liveness probe invoke.");

        var result = await Task.FromResult(Ok("Healthy."));

        return result;
    }

    [HttpGet]
    [Route("health/ready")]
    [SwaggerOperation(Summary = "Readiness probe.")]
    public async Task<IActionResult> Ready()
    {
        _logger.LogInformation("Readiness probe invoke.");

        var result = await Task.FromResult(Ok("Ready."));

        return result;
    }
}