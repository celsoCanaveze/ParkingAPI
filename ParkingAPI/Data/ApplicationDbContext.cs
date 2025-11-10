using Microsoft.EntityFrameworkCore;
using ParkingAPI.Models;
using System.Collections.Generic;
using System.IO;
using ReservaTest = ParkingAPI.Models.Reserva;

namespace ParkingAPI.Data
{
    // No arquivo ApplicationDbContext.cs
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; } // Mantido corretamente
    }

}
