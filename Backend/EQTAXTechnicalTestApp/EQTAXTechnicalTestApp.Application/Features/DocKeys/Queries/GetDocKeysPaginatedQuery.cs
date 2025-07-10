using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Entities;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Queries
{
    public record GetDocKeysPaginatedQuery( int PageNumber = 1, int PageSize = 10, string? SearchTerm = null) : IRequest<PaginatedResult<DocKey>>;
}