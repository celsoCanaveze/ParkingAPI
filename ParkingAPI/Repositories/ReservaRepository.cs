using ParkingAPI.Data;
using ParkingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Repositories
{
    public class ReservaRepository : CRUDRepository<Reserva>, IReservaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetReservasPorUsuario(int usuarioId)
        {
            return await _context.Set<Reserva>()
                .Include(r => r.Moto)
                .Include(r => r.Patio)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}