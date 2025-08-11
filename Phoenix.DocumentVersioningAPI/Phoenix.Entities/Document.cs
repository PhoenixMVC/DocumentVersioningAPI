using Phoenix.Entities;

namespace Phoenix.Entities
{
    public class Document : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<DocumentVersion> Versions { get; set; } = new List<DocumentVersion>();
    }
}
