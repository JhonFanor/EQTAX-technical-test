using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Queries
{
    public class GetAllLogProcessQueryHandler : IRequestHandler<GetAllLogProcessQuery, IEnumerable<LogProcess>>
    {
        private readonly ILogProcessRepository _repository;
        
        public GetAllLogProcessQueryHandler(ILogProcessRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LogProcess>> Handle(GetAllLogProcessQuery request, CancellationToken ct)
        {
            return await _repository.GetAllAsync();
        }
    }
}