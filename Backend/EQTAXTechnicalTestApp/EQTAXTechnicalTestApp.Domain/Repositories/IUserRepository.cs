using EQTAXTechnicalTestApp.Domain.Entities;

namespace EQTAXTechnicalTestApp.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsername(string username);
        Task AddRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken?> GetRefreshToken(string token);
        Task RevokeRefreshToken(string token);
    }
}