namespace DocumentProcessingApp.Application.DTOs.Requests
{
    public class DocKeyResponse
    {
        public int Id { get; set; }
        public required string DocName { get; set; }
        public required string Key { get; set; }
    }
}