using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RawSharer.Shared.Entities.Library
{
    public class Track : EntityBase
    {
        [Required]
        [MaxLength(128)]
        [Index(IsClustered = false, IsUnique = false)]
        public string Name { get; set; }
        public byte? DiskNumber { get; set; }
        public byte? TrackNumber { get; set; }
        public TimeSpan? Duration { get; set; }

        public virtual Album Album { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<TrackVersion> Versions { get; set; }

        public Track(string name, byte? diskNumber = null,
            byte? trackNumber = null, TimeSpan? duration = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            DiskNumber = diskNumber;
            TrackNumber = trackNumber;
            Duration = duration;

            Artists = new List<Artist>();
            Versions = new List<TrackVersion>();
        }

        public Track()
        {
            // Reserved for Serialization
        }
    }
}