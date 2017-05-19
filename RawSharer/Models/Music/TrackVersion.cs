using System;
using System.ComponentModel.DataAnnotations;
using RawSharer.Models.BaseClasses;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Music
{
    public class TrackVersion : Entity
    {
        [MaxLength(128)]
        public string Name { get; set; }

        public virtual Track Track { get; set; }
        public virtual LocalBlob OriginalStorage { get; set; }
        public virtual LocalBlob ConvertedStorage { get; set; }
        public virtual Lyrics.Lyrics Lyrics { get; set; }

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