using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Commands
{
    public record DeleteLogProcessCommand(int Id) : IRequest<Unit>;
}