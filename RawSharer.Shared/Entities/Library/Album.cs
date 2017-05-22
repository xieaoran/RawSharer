using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RawSharer.Shared.Entities.Storage;

namespace RawSharer.Shared.Entities.Library
{
    public class Album: EntityBase
    {
        [Required]
        [MaxLength(128)]
        [Index(IsClustered = false, IsUnique = false)]
        public string Name { get; set; }
        [MaxLength(16)]
        public string ReleaseDate { get; set; }
        public byte? DiskCount { get; set; }
        public byte? TrackCount { get; set; }

        [MaxLength(128)]
        [Index(IsClustered = false, IsUnique = false)]
        public string Genre { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
        public virtual BlobStorage ImageStorage { get; set; }

        public Album(string name,
            string releaseDate = null, byte? diskCount = null,
            byte? trackCount = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            ReleaseDate = releaseDate;
            DiskCount = diskCount;
            TrackCount = trackCount;

            Artists = new List<Artist>();
            Tracks = new List<Track>();
        }

        public Album()
        {
            // Reserved for Serialization
        }
    }
}