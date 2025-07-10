using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Commands
{
    public record DeleteDocKeyCommand(int Id) : IRequest<Unit>;
}