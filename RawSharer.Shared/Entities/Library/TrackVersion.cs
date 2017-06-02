using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RawSharer.Shared.Entities.Storage;

namespace RawSharer.Shared.Entities.Library
{
    public class TrackVersion : EntityBase
    {
        [MaxLength(128)]
        [Index(IsClustered = false, IsUnique = false)]
        public string Name { get; set; }

        public virtual Track Track { get; set; }
        public virtual BlobStorage OriginalStorage { get; set; }
        public virtual BlobStorage ConvertedStorage { get; set; }
        public virtual Lyrics Lyrics { get; set; }

        public TrackVersion(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public TrackVersion()
        {
            // Reserved For Serialization
        }
    }
}