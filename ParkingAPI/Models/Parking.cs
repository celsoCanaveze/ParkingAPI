namespace ParkingAPI.Models
{
    public class Parking
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
