using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.DocKeys.Commands
{
    public class DeleteDocKeyCommandHandler : IRequestHandler<DeleteDocKeyCommand, Unit>
    {
        private readonly IDocKeyRepository _repository;
        
        public DeleteDocKeyCommandHandler(IDocKeyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteDocKeyCommand request, CancellationToken ct)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}