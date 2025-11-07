using Microsoft.EntityFrameworkCore;
using ParkingApi.Models;
using System.Collections.Generic;
using System.IO;
using ReservaTest = ParkingApi.Models.Reserva;

namespace ParkingApi.Data
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
