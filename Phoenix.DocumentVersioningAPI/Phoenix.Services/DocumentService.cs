using Phoenix.Data.Contracts;
using Phoenix.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Phoenix.Services
{
    public class DocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IStorageService _storageService;

        public DocumentService(IDocumentRepository documentRepository, IStorageService storageService)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public async Task<Document> CreateDocumentAsync(string title, string description)
        {
            var document = new Document
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description
            };

            await _documentRepository.AddAsync(document, default);
            return document;
        }

        public async Task<DocumentVersion> AddVersionAsync(Guid documentId, Stream fileStream, string fileName, string notes)
        {
            var document = _documentRepository.GetByIdWithVersions(documentId);
            if (document == null)
                throw new Exception("Document not found");

            // ????? ???? ?? ???? Async
            var filePath = await _storageService.SaveFileAsync(fileStream, fileName);

            var versionNumber = document.Versions.Count + 1;

            var version = new DocumentVersion
            {
                Id = Guid.NewGuid(),
                VersionNumber = versionNumber,
                FilePath = filePath,
                Notes = notes,
                CreatedAt = DateTime.UtcNow,
                DocumentId = document.Id
            };

            document.Versions.Add(version);
            await _documentRepository.UpdateAsync(document, default);
            return version;
        }

        public Task<List<Document>> GetAllDocumentsAsync()
        {
            return Task.FromResult(_documentRepository.Table.ToList());
        }
    }
}
