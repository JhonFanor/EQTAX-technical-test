using EQTAXTechnicalTestApp.Application.DTOs.Requests;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Commands
{
    public record UpdateLogProcessCommand(LogProcessRequest Request) : IRequest<Unit>;
}