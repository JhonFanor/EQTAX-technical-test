using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.Auth.Commands
{
    public record RefreshTokenCommand(string Token) : IRequest<AuthResponse>;
}