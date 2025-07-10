using EQTAXTechnicalTestApp.Domain.Entities;
using MediatR;

namespace EQTAXTechnicalTestApp.Application.Features.LogProcesses.Queries
{
    public record GetLogProcessByIdQuery(int Id) : IRequest<LogProcess>;
}