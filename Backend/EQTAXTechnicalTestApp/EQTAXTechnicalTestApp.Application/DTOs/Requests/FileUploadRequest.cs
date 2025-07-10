
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EQTAXTechnicalTestApp.Application.DTOs.Requests
{
    public class FileUploadRequest
    {
        [FromForm(Name = "file")]
        public required IFormFile File { get; set; }
    }
}