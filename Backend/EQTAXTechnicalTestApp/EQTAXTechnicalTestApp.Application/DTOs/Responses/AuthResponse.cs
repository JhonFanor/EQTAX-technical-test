namespace EQTAXTechnicalTestApp.Application.DTOs.Responses
{
    public class AuthResponse
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}