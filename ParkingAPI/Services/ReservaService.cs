using AutoMapper;
using ParkingApi.DTOs;
using ParkingApi.Models;
using ParkingApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingApi.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _repo;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservaDto>> GetAllAsync()
        {
            var reservas = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservaDto>>(reservas);
        }

        public async Task<ReservaDto> GetByIdAsync(int id)
        {
            var reserva = await _repo.GetByIdAsync(id);
            return _mapper.Map<ReservaDto>(reserva);
        }

        public async Task<ReservaDto> CreateAsync(ReservaDto dto)
        {
            var reserva = _mapper.Map<Reserva>(dto);
            await _repo.AddAsync(reserva);
            return _mapper.Map<ReservaDto>(reserva);
        }

        public async Task<ReservaDto> UpdateAsync(int id, ReservaDto dto)
        {
            var reserva = await _repo.GetByIdAsync(id);
            if (reserva == null) return null;

            _mapper.Map(dto, reserva);
            await _repo.UpdateAsync(reserva);
            return _mapper.Map<ReservaDto>(reserva);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reserva = await _repo.GetByIdAsync(id);
            if (reserva == null) return false;

            await _repo.DeleteAsync(id); 
            return true;
        }


    }
}
