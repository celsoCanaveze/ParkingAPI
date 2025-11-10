using ParkingAPI.Models;

namespace ParkingAPI.DTOs
{
    public class ParkingDto
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public Moto? Moto { get; set; }
        public int PatioId { get; set; }
        public Patio? Patio { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
    }
}
