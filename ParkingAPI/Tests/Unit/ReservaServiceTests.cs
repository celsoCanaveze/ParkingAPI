using Xunit;
using ParkingAPI.Services;
using ParkingAPI.Models;

namespace ParkingAPI.Tests.Unit
{
    public class ReservaServiceTests
    {
        [Fact]
        public void DeveCalcularValorReservaCorretamente()
        {
            var service = new ReservaService();
            var valor = service.CalcularValor(2, 10);
            Assert.Equal(20, valor);
        }
    }
}
