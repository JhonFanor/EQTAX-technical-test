

using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Queries
{
    public class GetLogProcessPaginatedQueryHandler : IRequestHandler<GetLogProcessPaginatedQuery, PaginatedResult<LogProcess>>
    {
        private readonly ILogProcessRepository _repository;
        
        public GetLogProcessPaginatedQueryHandler(ILogProcessRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<LogProcess>> Handle( GetLogProcessPaginatedQuery request, CancellationToken ct )
        {
            return await _repository.GetPaginatedAsync(
                request.PageNumber, 
                request.PageSize, 
                request.SearchTerm);
        }
    }
}