using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Queries
{
    public class GetAllDocKeysQueryHandler : IRequestHandler<GetAllDocKeysQuery, IEnumerable<DocKey>>
    {
        private readonly IDocKeyRepository _repository;
        
        public GetAllDocKeysQueryHandler(IDocKeyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DocKey>> Handle(GetAllDocKeysQuery request, CancellationToken ct)
        {
            return await _repository.GetAllAsync();
        }
    }
}