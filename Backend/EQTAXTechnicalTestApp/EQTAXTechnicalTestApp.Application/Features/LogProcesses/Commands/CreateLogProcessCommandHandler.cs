using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Commands
{
    public class CreateLogProcessCommandHandler : IRequestHandler<CreateLogProcessCommand, int>
    {
        private readonly ILogProcessRepository _repository;
        public CreateLogProcessCommandHandler(ILogProcessRepository repository) => _repository = repository;

        public async Task<int> Handle(CreateLogProcessCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var logProcess = new LogProcess { OriginalFileName = request.OriginalFileName, 
                                               Status = request.Status, 
                                               NewFileName = request.NewFileName, 
                                               DateProcess = request.DateProcess };
            await _repository.AddAsync(logProcess);
            return logProcess.Id;
        }
    }
}
