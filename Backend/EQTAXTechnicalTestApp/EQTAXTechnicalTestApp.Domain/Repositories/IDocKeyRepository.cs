using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Entities;

namespace EQTAXTechnicalTestApp.Domain.Repositories
{
    public interface IDocKeyRepository : IRepository<DocKey>
    {
        Task<PaginatedResult<DocKey>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm = null);
    }

}