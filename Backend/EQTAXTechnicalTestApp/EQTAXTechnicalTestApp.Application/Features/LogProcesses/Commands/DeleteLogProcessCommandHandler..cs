using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Commands
{
    public class DeleteLogProcessCommandHandler : IRequestHandler<DeleteLogProcessCommand, Unit>
    {
        private readonly ILogProcessRepository _repository;
        
        public DeleteLogProcessCommandHandler(ILogProcessRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteLogProcessCommand request, CancellationToken ct)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}