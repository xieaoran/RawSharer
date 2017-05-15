using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RawSharer.Configs;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Music
{
    public class Album
    {
        [Key]
        public Guid Id { get; }
        [Required]
        public string Name { get; set; }
        public string ReleaseDate { get; set; }
        public byte? DiskCount { get; set; }
        public byte? TrackCount { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual LocalBlob Image { get; set; }

        public Album(string name,
            string releaseDate = null, byte? diskCount = null,
            byte? trackCount = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            ReleaseDate = releaseDate;
            DiskCount = diskCount;
            TrackCount = trackCount;
        }

        public string GetArtists()
        {
            return string.Join(RuntimeConfig.Config.Format.ArtistSeparator, Artists.Select(artist => artist.Name));
        }

        public Album()
        {
            // Reserved for Serialization
        }
    }
}