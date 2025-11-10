using Microsoft.AspNetCore.Mvc;
using ParkingAPI.Models;
using ParkingAPI.Services;

namespace ParkingAPI.Controllers
{
    [ApiController]
    [Route("api/v1/ml")]
    public class MLController : ControllerBase
    {
        private readonly MLService _mlService;

        public MLController(MLService mlService)
        {
            _mlService = mlService;
        }

        [HttpPost("predict")]
        public IActionResult Predict([FromBody] PredictInput input)
        {
            var result = _mlService.Predict(input);
            return Ok(result);
        }
    }
}
