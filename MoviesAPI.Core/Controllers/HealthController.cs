using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MoviesAPI.Core.Controllers;

[ApiController]
public class HealthController : Controller
{
    [HttpGet]
    [Route("health/live")]
    [SwaggerOperation(Summary = "Liveness probe.")]
    public async Task<IActionResult> Alive()
    {
        var result = await Task.FromResult(Ok("Healthy."));

        return result;
    }

    [HttpGet]
    [Route("health/ready")]
    [SwaggerOperation(Summary = "Readiness probe.")]
    public async Task<IActionResult> Ready()
    {
        var result = await Task.FromResult(Ok("Ready."));

        return result;
    }
}