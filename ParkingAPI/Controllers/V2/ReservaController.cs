using Microsoft.AspNetCore.Mvc;
using ParkingAPI.DTOs;
using ParkingAPI.Services;

namespace ParkingAPI.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _service;

        public ReservaController(IReservaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reserva = await _service.GetByIdAsync(id);
            return reserva == null ? NotFound() : Ok(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReservaDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
