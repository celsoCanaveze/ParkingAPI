using ParkingAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Services
{
	public interface IReservaService
	{
		Task<IEnumerable<ReservaDto>> GetAllAsync();
		Task<ReservaDto> GetByIdAsync(int id);
		Task<ReservaDto> CreateAsync(ReservaDto dto);
		Task<ReservaDto> UpdateAsync(int id, ReservaDto dto);
		Task<bool> DeleteAsync(int id);
	}
}
