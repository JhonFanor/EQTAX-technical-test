using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Queries
{
    public class GetDocKeyByIdQueryHandler : IRequestHandler<GetDocKeyByIdQuery, DocKey>
    {
        private readonly IDocKeyRepository _repository;
        
        public GetDocKeyByIdQueryHandler(IDocKeyRepository repository)
        {
            _repository = repository;
        }

        public async Task<DocKey> Handle(GetDocKeyByIdQuery request, CancellationToken ct)
        {
            return await _repository.GetByIdAsync(request.Id) 
                ?? throw new KeyNotFoundException($"DocKey con ID {request.Id} no encontrado");
        }
    }
}