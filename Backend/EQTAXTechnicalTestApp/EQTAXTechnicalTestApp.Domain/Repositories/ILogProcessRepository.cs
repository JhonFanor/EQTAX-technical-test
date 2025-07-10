using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Entities;

namespace EQTAXTechnicalTestApp.Domain.Repositories
{
    public interface ILogProcessRepository : IRepository<LogProcess>
    {
        Task<PaginatedResult<LogProcess>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm = null);
    }
}