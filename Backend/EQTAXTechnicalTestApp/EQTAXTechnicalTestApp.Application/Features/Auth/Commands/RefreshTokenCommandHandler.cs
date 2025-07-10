using MediatR;
using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Repositories;
using EQTAXTechnicalTestApp.Domain.Services;

namespace EQTAXTechnicalTestApp.Application.Features.Auth.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler( IUserRepository userRepository, IJwtService jwtService, IAuthService authService )
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var existingRefreshToken = await _userRepository.GetRefreshToken(request.Token);
            if (existingRefreshToken == null)
            {
                throw new UnauthorizedAccessException("Refresh token no encontrado");
            }

            if (existingRefreshToken.Expiration < DateTime.UtcNow)
            {
                await _userRepository.RevokeRefreshToken(request.Token);
                throw new UnauthorizedAccessException("Refresh token expirado");
            }

            var newAccessToken = _jwtService.GenerateToken(existingRefreshToken.User);

            await _userRepository.RevokeRefreshToken(request.Token);

            var newRefreshToken = await _authService.GenerateRefreshToken(existingRefreshToken.UserId);

            return new AuthResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }
    }
}
