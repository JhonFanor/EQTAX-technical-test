using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using EQTAXTechnicalTestApp.Domain.Services;

namespace EQTAXTechnicalTestApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService( IUserRepository userRepository, IJwtService jwtService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsername(username);
           
            if (user == null || !_passwordHasher.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return _jwtService.GenerateToken(user);
        }

        public async Task<string> GenerateRefreshToken(int userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(7),
                UserId = userId
            };

            await _userRepository.AddRefreshToken(refreshToken);
            return refreshToken.Token;
        }

        public async Task<string> RefreshToken(string token)
        {
            var refreshToken = await _userRepository.GetRefreshToken(token);
            if (refreshToken == null || refreshToken.Expiration < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            await _userRepository.RevokeRefreshToken(token);
            return await GenerateRefreshToken(refreshToken.UserId);
        }
    }
}