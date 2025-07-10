using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Commands
{
    public class UpdateLogProcessCommandHandler : IRequestHandler<UpdateLogProcessCommand, Unit>
    {
        private readonly ILogProcessRepository _repository;
        
        public UpdateLogProcessCommandHandler(ILogProcessRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateLogProcessCommand command, CancellationToken ct)
        {
            var request = command.Request;
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"DocKey con ID {request.Id} no encontrado");
            }

            entity.OriginalFileName = request.OriginalFileName;
            entity.Status = request.Status;
            entity.NewFileName = request.NewFileName;
            entity.DateProcess = request.DateProcess;

            await _repository.UpdateAsync(entity);
            return Unit.Value;
        }
    }
}