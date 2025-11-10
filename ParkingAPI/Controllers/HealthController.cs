using Microsoft.AspNetCore.Mvc;

namespace ParkingAPI.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "Healthy" });
    }
}
