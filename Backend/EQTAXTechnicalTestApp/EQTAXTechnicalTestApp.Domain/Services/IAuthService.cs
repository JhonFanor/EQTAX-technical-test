namespace EQTAXTechnicalTestApp.Domain.Services
{
    public interface IAuthService
    {
        Task<string> Authenticate(string username, string password);
        Task<string> GenerateRefreshToken(int userId);
        Task<string> RefreshToken(string token);
    }
}