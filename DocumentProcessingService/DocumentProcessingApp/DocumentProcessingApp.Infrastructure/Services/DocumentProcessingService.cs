using DocumentProcessingApp.Application.DTOs.Requests;
using DocumentProcessingApp.Application.Interfaces;
using DocumentProcessingApp.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;

namespace DocumentProcessingApp.Infrastructure.Services
{
    public class DocumentProcessingService
    {
        private readonly IDocKeyService _docKeyService;
        private readonly ILogProcessService _logProcessService;
        private readonly string _inputFolder;
        private readonly string _ocrFolder;
        private readonly string _unknownFolder;

        public DocumentProcessingService(IDocKeyService docKeyService, ILogProcessService logProcessService, IConfiguration config)
        {
            _docKeyService = docKeyService;
            _logProcessService = logProcessService;

            _inputFolder = config["FileSettings:Input"];
            _ocrFolder = config["FileSettings:Ocr"];
            _unknownFolder = config["FileSettings:Unknown"];
        }

        public async Task ProcessFilesAsync()
        {
            Directory.CreateDirectory(_inputFolder);
            Directory.CreateDirectory(_ocrFolder);
            Directory.CreateDirectory(_unknownFolder);

            var files = Directory.GetFiles(_inputFolder, "*.pdf");

            if (files.Length == 0)
            {
                Console.WriteLine("📂 No se encontraron archivos PDF para procesar.");
                return;
            }

            var docKeys = await _docKeyService.GetAllDocKeysAsync();


            if (docKeys == null || !docKeys.Any())
            {
                Console.WriteLine("⚠️  No hay DocKeys registrados. El proceso no puede continuar.");
                return;
            }

            foreach (var file in files)
            {
                string text = PdfTextExtractorHelper.ExtractText(file);
                var match = docKeys.FirstOrDefault(k => text.Contains(k.Key, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                {
                    string newPath = Path.Combine(_ocrFolder, $"{match.DocName}_{Path.GetFileName(file)}");
                    File.Move(file, newPath);

                    await _logProcessService.CreateAsync(new LogProcessRequest
                    {
                        OriginalFileName = Path.GetFileName(file),
                        NewFileName = newPath,
                        Status = "Processed",
                        DateProcess = DateTime.UtcNow
                    });
                }
                else
                {
                    string newPath = Path.Combine(_unknownFolder, Path.GetFileName(file));
                    File.Move(file, newPath);

                    await _logProcessService.CreateAsync(new LogProcessRequest
                    {
                        OriginalFileName = Path.GetFileName(file),
                        NewFileName = "",
                        Status = "Unknown",
                        DateProcess = DateTime.UtcNow
                    });
                }
            }
        }
    }
}