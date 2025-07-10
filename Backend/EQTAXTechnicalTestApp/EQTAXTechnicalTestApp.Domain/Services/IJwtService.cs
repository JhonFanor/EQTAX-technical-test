using EQTAXTechnicalTestApp.Domain.Entities;

namespace EQTAXTechnicalTestApp.Domain.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}