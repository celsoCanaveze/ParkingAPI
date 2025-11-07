namespace ParkingApi.Models
{
    public class Moto
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
