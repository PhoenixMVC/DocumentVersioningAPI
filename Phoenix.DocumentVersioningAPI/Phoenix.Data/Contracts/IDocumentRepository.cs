using Phoenix.Entities;

namespace Phoenix.Data.Contracts
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Document GetByIdWithVersions(Guid id);
    }
}
