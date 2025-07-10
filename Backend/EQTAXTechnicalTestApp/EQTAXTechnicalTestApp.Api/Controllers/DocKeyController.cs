using EQTAXTechnicalTestApp.Application.DTOs.Requests;
using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Application.Features.DocKeys.Commands;
using EQTAXTechnicalTestApp.Application.Features.DocKeys.Queries;
using EQTAXTechnicalTestApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EQTAXTechnicalTestApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocKeysController : ControllerBase
    {
        private readonly IMediator _mediator;
    
        public DocKeysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<DocKey>>> GetPaginated( [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            var result = await _mediator.Send(new GetDocKeysPaginatedQuery( pageNumber, pageSize, searchTerm ));
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DocKey>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllDocKeysQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocKey>> GetById(int id)
        {
            var result = await _mediator.Send(new GetDocKeyByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] DocKeyRequest request)
        {
            var command = new CreateDocKeyCommand(request);
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DocKeyRequest request)
        {
            request.Id = id; 
            var command = new UpdateDocKeyCommand(request);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteDocKeyCommand(id));
            return NoContent();
        }
    }
}
