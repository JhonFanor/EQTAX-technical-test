using DocumentProcessingApp.Application.DTOs.Requests;

namespace DocumentProcessingApp.Application.Interfaces
{
    public interface ILogProcessService
    {
        Task CreateAsync(LogProcessRequest request);
    }

}