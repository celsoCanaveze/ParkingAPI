using ParkingApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingApi.Repositories
{
    public interface IReservaRepository : ICRUDRepository<Reserva>
    {
        Task<IEnumerable<Reserva>> GetReservasPorUsuario(int usuarioId);
    }
}
