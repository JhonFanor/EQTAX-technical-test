using Microsoft.AspNetCore.Http;

namespace EQTAXTechnicalTestApp.Domain.Services
{
    public interface IFileStorageService
    {
        Task<string> SavePdfAsync(IFormFile file);
    }
}