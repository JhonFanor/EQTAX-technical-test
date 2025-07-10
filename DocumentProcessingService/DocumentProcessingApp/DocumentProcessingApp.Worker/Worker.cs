using DocumentProcessingApp.Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DocumentProcessingApp.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DocumentProcessingService _processingService;
        private readonly IHttpClientFactory _httpClientFactory;

        public Worker(ILogger<Worker> logger, DocumentProcessingService processingService, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _processingService = processingService;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Document Processor Worker started at: {time}", DateTimeOffset.Now);

            await EsperarApiDisponibleAsync("http://eqtax-api:8080/health");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Starting document processing at: {time}", DateTimeOffset.Now);
                    await _processingService.ProcessFilesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during document processing");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
            }

            _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);
        }

        private async Task EsperarApiDisponibleAsync(string url)
        {
            var client = _httpClientFactory.CreateClient();
            int maxRetries = 10;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("✅ API disponible en intento {attempt}", attempt);
                        return;
                    }

                    _logger.LogWarning("⏳ API respondió con código {code} en intento {attempt}", response.StatusCode, attempt);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("⏳ Esperando API... intento {attempt}. Error: {error}", attempt, ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            throw new Exception("❌ El API no está disponible después de varios intentos.");
        }
    }
}
