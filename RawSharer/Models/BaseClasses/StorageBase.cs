using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace RawSharer.Models.BaseClasses
{
    public enum StorageType
    {
        Audio,
        Video,
        Image,
        Lyrics,
        Other
    }
    public abstract class StorageBase : Entity
    {
        [Required]
        public StorageType StorageType { get; protected set; }
        [Required]
        [MaxLength(64)]
        public string ContentType { get; protected set; }
        [Required]
        [MaxLength(32)]
        [Index(IsClustered = false, IsUnique = true)]
        public string Md5Hash { get; protected set; }
        public long Length { get; protected set; }

        public abstract Stream GetReadStream();
        public abstract Stream GetWriteStream();
        public abstract void HandleUpload(Stream uploadStream);
    }
}