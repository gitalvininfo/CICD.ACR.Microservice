using Microsoft.AspNetCore.Mvc;

namespace K8S.DriverStatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverStatsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { success = true, message = "Connection established... 🔥🔥🔥" });
        }
    }
}
