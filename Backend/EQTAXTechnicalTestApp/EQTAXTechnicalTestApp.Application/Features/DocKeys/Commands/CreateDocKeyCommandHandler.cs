using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Commands
{
    public class CreateDocKeyCommandHandler : IRequestHandler<CreateDocKeyCommand, int>
    {
        private readonly IDocKeyRepository _repository;
        public CreateDocKeyCommandHandler(IDocKeyRepository repository) => _repository = repository;

        public async Task<int> Handle(CreateDocKeyCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var docKey = new DocKey { DocName = request.DocName, Key = request.Key };
            await _repository.AddAsync(docKey);
            return docKey.Id;
        }
    }
}