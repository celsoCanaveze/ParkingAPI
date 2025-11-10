using ParkingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories
{
    public interface IReservaRepository : ICRUDRepository<Reserva>
    {
        Task<IEnumerable<Reserva>> GetReservasPorUsuario(int usuarioId);
    }
}
