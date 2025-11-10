namespace ParkingAPI.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int MotoId { get; set; }
        public int PatioId { get; set; }
        public DateTime DataReserva { get; set; }
        public DateTime? DataSaida { get; set; }

        
        public Usuario Usuario { get; set; }
        public Moto Moto { get; set; }
        public Patio Patio { get; set; }
    }
}
