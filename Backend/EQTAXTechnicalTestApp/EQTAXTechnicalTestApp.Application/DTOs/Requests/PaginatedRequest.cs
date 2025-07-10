namespace EQTAXTechnicalTestApp.Application.DTOs.Requests
{
    public record PaginatedRequest(int PageNumber = 1, int PageSize = 10);
}
