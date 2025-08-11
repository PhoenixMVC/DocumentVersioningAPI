using Phoenix.Entities;

namespace Phoenix.Entities
{
    public class DocumentVersion : BaseEntity<Guid>
    {
        public int VersionNumber { get; set; }
        public string FilePath { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
