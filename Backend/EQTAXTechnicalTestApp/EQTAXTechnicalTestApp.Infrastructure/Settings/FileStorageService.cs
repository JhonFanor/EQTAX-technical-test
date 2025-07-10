using EQTAXTechnicalTestApp.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EQTAXTechnicalTestApp.Infrastructure.Settings
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _uploadPath;

        public FileStorageService(IConfiguration configuration)
        {
            _uploadPath = configuration["FileUploadSettings:UploadFolder"]
                        ?? throw new InvalidOperationException("Upload folder path not configured.");
        }

        public async Task<string> SavePdfAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Archivo vacío.");

            if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
                throw new ArgumentException("Solo se permiten archivos PDF.");

            const long maxSize = 10 * 1024 * 1024;
            if (file.Length > maxSize)
                throw new ArgumentException("El archivo excede el tamaño máximo permitido (10 MB).");

            using var reader = file.OpenReadStream();
            var buffer = new byte[4];
            await reader.ReadAsync(buffer, 0, 4);
            if (buffer[0] != 0x25 || buffer[1] != 0x50 || buffer[2] != 0x44 || buffer[3] != 0x46)
                throw new ArgumentException("El archivo no es un PDF válido.");

            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);

            var fileName = Path.GetFileName(file.FileName);
            var fullPath = Path.Combine(_uploadPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fullPath;
        }
    }
}