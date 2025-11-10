using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ParkingAPI.Tests.Integration
{
    public class HealthCheckTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HealthCheckTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DeveRetornarStatusHealthy()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/health");
            var content = await response.Content.ReadAsStringAsync();

            Assert.Contains("Healthy", content);
        }
    }
}
