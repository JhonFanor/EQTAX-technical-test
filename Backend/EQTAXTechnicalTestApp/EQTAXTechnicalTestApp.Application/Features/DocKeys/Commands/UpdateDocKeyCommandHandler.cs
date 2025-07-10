using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Commands
{
    public class UpdateDocKeyCommandHandler : IRequestHandler<UpdateDocKeyCommand, Unit>
    {
        private readonly IDocKeyRepository _repository;
        
        public UpdateDocKeyCommandHandler(IDocKeyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateDocKeyCommand command, CancellationToken ct)
        {
            var request = command.Request;
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"DocKey con ID {request.Id} no encontrado");
            }

            entity.DocName = request.DocName;
            entity.Key = request.Key;

            await _repository.UpdateAsync(entity);
            return Unit.Value;
        }
    }
}