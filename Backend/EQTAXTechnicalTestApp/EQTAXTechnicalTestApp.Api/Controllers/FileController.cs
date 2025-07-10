using EQTAXTechnicalTestApp.Application.DTOs.Requests;
using EQTAXTechnicalTestApp.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace EQTAXTechnicalTestApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;

        public FileController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<IActionResult> Upload([FromForm] FileUploadRequest request)
        {
            try
            {
                var savedPath = await _fileStorageService.SavePdfAsync(request.File);
                return Ok(new { path = savedPath });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}