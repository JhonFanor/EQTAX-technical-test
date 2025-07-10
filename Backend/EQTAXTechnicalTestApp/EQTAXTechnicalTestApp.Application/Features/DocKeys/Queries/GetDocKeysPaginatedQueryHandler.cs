using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Queries
{
    public class GetDocKeysPaginatedQueryHandler : IRequestHandler<GetDocKeysPaginatedQuery, PaginatedResult<DocKey>>
    {
        private readonly IDocKeyRepository _repository;
        
        public GetDocKeysPaginatedQueryHandler(IDocKeyRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<DocKey>> Handle( GetDocKeysPaginatedQuery request, CancellationToken ct )
        {
            return await _repository.GetPaginatedAsync(
                request.PageNumber, 
                request.PageSize, 
                request.SearchTerm);
        }
    }
}