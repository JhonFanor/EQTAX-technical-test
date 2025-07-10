using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Queries
{
    public class GetLogProcessByIdQueryHandler : IRequestHandler<GetLogProcessByIdQuery, LogProcess>
    {
        private readonly ILogProcessRepository _repository;
        
        public GetLogProcessByIdQueryHandler(ILogProcessRepository repository)
        {
            _repository = repository;
        }

        public async Task<LogProcess> Handle(GetLogProcessByIdQuery request, CancellationToken ct)
        {
            return await _repository.GetByIdAsync(request.Id) 
                ?? throw new KeyNotFoundException($"DocKey con ID {request.Id} no encontrado");
        }
    }
}