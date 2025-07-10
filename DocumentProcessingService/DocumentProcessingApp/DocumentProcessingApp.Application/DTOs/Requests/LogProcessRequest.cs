namespace DocumentProcessingApp.Application.DTOs.Requests
{
    public class LogProcessRequest
    {
        public int Id { get; set; }
        public required string OriginalFileName { get; set; }
        public required string Status { get; set; }
        public string? NewFileName { get; set; }
        public required DateTime DateProcess { get; set; } 
    }
}