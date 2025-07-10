using EQTAXTechnicalTestApp.Domain.Entities;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Queries
{
    public record GetAllDocKeysQuery : IRequest<IEnumerable<DocKey>>;
}