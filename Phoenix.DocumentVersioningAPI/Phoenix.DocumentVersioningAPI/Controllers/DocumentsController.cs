using Microsoft.AspNetCore.Mvc;
using Phoenix.Entities;
using Phoenix.Services;


namespace Phoenix.DocumentVersioningAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentsController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public ActionResult<Document> CreateDocument([FromForm] string title, [FromForm] string description)
        {
            var doc = _documentService.CreateDocumentAsync(title, description);
            return Ok(doc);
        }

        [HttpPost("{id:guid}/versions")]
        public ActionResult<DocumentVersion> AddVersion(Guid id, IFormFile file, [FromForm] string notes)
        {
            using var stream = file.OpenReadStream();
            var version = _documentService.AddVersionAsync(id, stream, file.FileName, notes);
            return Ok(version);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Document>> GetAllDocuments()
        {
            return Ok(_documentService.GetAllDocumentsAsync());
        }
    }
}
