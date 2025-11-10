namespace ParkingAPI.Services
{
    public interface IAuthService
    {
        string GenerateToken(string username);
    }
}
