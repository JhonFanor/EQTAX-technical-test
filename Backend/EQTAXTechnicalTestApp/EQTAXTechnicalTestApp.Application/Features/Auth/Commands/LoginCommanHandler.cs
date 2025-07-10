using AutoMapper;
using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Repositories;
using EQTAXTechnicalTestApp.Domain.Services;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginCommandHandler( IAuthService authService, IUserRepository userRepository,IMapper mapper)
        {
            _authService = authService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            
            var token = await _authService.Authenticate(request.Username, request.Password);
            
            var user = await _userRepository.GetByUsername(request.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            
            var refreshToken = await _authService.GenerateRefreshToken(user.Id);
            
            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}