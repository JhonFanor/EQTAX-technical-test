namespace EQTAXTechnicalTestApp.Application.DTOs.Responses
{
    public class PaginatedResult<T>
    {
        public required List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}