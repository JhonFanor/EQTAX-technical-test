namespace EQTAXTechnicalTestApp.Application.DTOs.Requests
{
    public class DocKeyRequest
    {
        public int Id { get; set; }
        public required string DocName { get; set; }
        public required string Key { get; set; }
    }
}
