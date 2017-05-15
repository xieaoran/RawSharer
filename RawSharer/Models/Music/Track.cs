using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RawSharer.Configs;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Music
{
    public class Track
    {
        [Key]
        public Guid Id { get; }
        [Required]
        public string Name { get; set; }
        public byte? DiskNumber { get; set; }
        public byte? TrackNumber { get; set; }
        public TimeSpan? Duration { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }

        public Track(string name, byte? diskNumber = null,
            byte? trackNumber = null, TimeSpan? duration = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            DiskNumber = diskNumber;
            TrackNumber = trackNumber;
            Duration = duration;
        }

        public Track()
        {
            // Reserved for Serialization
        }
        public string GetArtists()
        {
            return string.Join(RuntimeConfig.Config.Format.ArtistSeparator,
                Artists.Select(artist => artist.Name));
        }
    }
}