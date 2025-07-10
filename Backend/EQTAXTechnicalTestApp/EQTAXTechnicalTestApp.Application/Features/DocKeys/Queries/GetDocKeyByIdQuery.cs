using EQTAXTechnicalTestApp.Domain.Entities;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Queries
{
    public record GetDocKeyByIdQuery(int Id) : IRequest<DocKey>;
}