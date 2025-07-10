namespace DocumentProcessingApp.Domain.Service
{
    public interface IDocKeyService
    {
        Task<List<DocKeyDto>> GetAllDocKeysAsync();
    }
}