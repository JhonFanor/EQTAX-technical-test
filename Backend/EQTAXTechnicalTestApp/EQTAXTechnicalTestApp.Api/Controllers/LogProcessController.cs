using EQTAXTechnicalTestApp.Application.DTOs.Requests;
using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Application.Features.LogProcesses.Commands;
using EQTAXTechnicalTestApp.Application.Features.LogProcesses.Queries;
using EQTAXTechnicalTestApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EQTAXTechnicalTestApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogProcessController : ControllerBase
    {
        private readonly IMediator _mediator;
    
        public LogProcessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<LogProcess>>> GetPaginated( [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            var result = await _mediator.Send(new GetLogProcessPaginatedQuery( pageNumber, pageSize, searchTerm ));
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DocKey>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllLogProcessQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocKey>> GetById(int id)
        {
            var result = await _mediator.Send(new GetLogProcessByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] LogProcessRequest request)
        {
            var command = new CreateLogProcessCommand(request);
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LogProcessRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID no coincide");
            request.Id = id; 
            var command = new UpdateLogProcessCommand(request);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLogProcessCommand(id));
            return NoContent();
        }
    }
}