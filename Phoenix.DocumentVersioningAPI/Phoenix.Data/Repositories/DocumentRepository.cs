using Microsoft.EntityFrameworkCore;
using Phoenix.Data.Contracts;
using Phoenix.Entities;

namespace Phoenix.Data.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public Document GetByIdWithVersions(Guid id)
        {
            return Table
      .Include(d => d.Versions)
      .FirstOrDefault(d => d.Id == id);
        }
    }
}
