namespace EQTAXTechnicalTestApp.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; } = 60;
    }
}