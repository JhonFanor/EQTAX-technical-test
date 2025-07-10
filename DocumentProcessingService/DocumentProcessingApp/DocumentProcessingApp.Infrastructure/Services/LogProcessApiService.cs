using DocumentProcessingApp.Application.DTOs.Requests;
using DocumentProcessingApp.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace DocumentProcessingApp.Infrastructure.Services
{
    public class LogProcessApiService : ILogProcessService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public LogProcessApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task CreateAsync(LogProcessRequest request)
        {
            var url = _config["Services:LogApiUrl"];
            var response = await _httpClient.PostAsJsonAsync(url, request);
            response.EnsureSuccessStatusCode();
        }
    }
}
