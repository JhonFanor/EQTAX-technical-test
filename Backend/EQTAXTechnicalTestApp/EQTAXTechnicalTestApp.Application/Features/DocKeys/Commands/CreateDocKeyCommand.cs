using EQTAXTechnicalTestApp.Application.DTOs.Requests;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Commands
{
    public record CreateDocKeyCommand(DocKeyRequest Request) : IRequest<int>;
}