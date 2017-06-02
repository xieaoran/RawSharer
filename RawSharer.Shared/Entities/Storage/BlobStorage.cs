using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RawSharer.Shared.Entities.Storage
{
    public class BlobStorage : EntityBase
    {
        [Required]
        [MaxLength(256)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(64)]
        public string MimeType { get; set; }

        [Required]
        [MaxLength(32)]
        [Index(IsClustered = false, IsUnique = true)]
        public string Md5Hash { get; set; }
        public long Length { get; set; }
    }
}