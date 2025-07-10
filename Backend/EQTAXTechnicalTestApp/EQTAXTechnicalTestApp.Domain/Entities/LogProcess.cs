namespace EQTAXTechnicalTestApp.Domain.Entities
{
    public class LogProcess
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? NewFileName { get; set; }
        public DateTime DateProcess { get; set; } = DateTime.UtcNow;
    }
}