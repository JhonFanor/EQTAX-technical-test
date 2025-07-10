using System.Net.Http.Json;
using DocumentProcessingApp.Application.DTOs.Requests;
using DocumentProcessingApp.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DocumentProcessingApp.Infrastructure.Services
{
    public class DocKeyApiService : IDocKeyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public DocKeyApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<DocKeyResponse>> GetAllDocKeysAsync()
        {
            var url = _config["Services:DocKeyApiUrl"];
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<List<DocKeyResponse>>();
            return result ?? new List<DocKeyResponse>();
        }
    }
}
