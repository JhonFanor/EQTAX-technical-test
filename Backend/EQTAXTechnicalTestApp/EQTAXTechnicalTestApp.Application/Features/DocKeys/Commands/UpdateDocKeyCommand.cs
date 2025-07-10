using EQTAXTechnicalTestApp.Application.DTOs.Requests;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Commands
{
    public record UpdateDocKeyCommand(DocKeyRequest Request) : IRequest<Unit>;
}