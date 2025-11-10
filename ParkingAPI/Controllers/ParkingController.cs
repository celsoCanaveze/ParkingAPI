using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAPI.DTOs;
using ParkingAPI.Models;
using ParkingAPI.Repositories;

namespace ParkingAPI.Controllers
{
    [Route("api/Parkings")]
    [ApiController]
    [Authorize]
    public class ParkingController : ControllerBase
    {
        private readonly ICRUDRepository<Parking> _repository;
        private readonly IMapper _mapper;

        public ParkingController(ICRUDRepository<Parking> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Parkings = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ParkingDto>>(Parkings));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Parking = await _repository.GetByIdAsync(id);
            if (Parking == null) return NotFound();
            return Ok(_mapper.Map<ParkingDto>(Parking));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ParkingDto dto)
        {
            var Parking = _mapper.Map<Parking>(dto);
            await _repository.AddAsync(Parking);
            return CreatedAtAction(nameof(Get), new { id = Parking.Id }, _mapper.Map<ParkingDto>(Parking));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ParkingDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var Parking = _mapper.Map<Parking>(dto);
            await _repository.UpdateAsync(Parking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
