namespace ParkingAPI.DTOs
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int MotoId { get; set; }
        public int PatioId { get; set; }
        public DateTime DataReserva { get; set; }
        public DateTime? DataSaida { get; set; }
    }
}
