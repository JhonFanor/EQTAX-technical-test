using DocumentProcessingApp.Application.DTOs.Requests;

namespace DocumentProcessingApp.Application.Interfaces
{
    public interface IDocKeyService
    {
        Task<List<DocKeyResponse>> GetAllDocKeysAsync();
    }
}