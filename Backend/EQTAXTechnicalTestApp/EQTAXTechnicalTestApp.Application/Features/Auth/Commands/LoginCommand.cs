using MediatR;
using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Application.DTOs.Requests;

namespace EQTAXTechnicalTestApp.Application.Features.Auth.Commands
{
    public record LoginCommand(LoginRequest Request) : IRequest<AuthResponse>;
}