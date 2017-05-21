using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using RawSharer.Configs;
using RawSharer.Models.Entities.Base;
using RawSharer.Models.Entities.Storage;

namespace RawSharer.Models.Entities.Music
{
    public class Album: Entity
    {
        [Required]
        [MaxLength(128)]
        [Index(IsClustered = false, IsUnique = false)]
        public string Name { get; set; }
        [MaxLength(16)]
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

            Artists = new List<Artist>();
            Tracks = new List<Track>();
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