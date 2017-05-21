using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RawSharer.Models.Entities.Base;
using RawSharer.Models.Entities.Storage;

namespace RawSharer.Models.Entities.Music
{
    public class Artist : Entity
    {
        [Required]
        [MaxLength(128)]
        [Index(IsClustered = false, IsUnique = false)]
        public string Name { get; set; }
        [MaxLength(1024)]
        public string Biography { get; set; }

        public virtual LocalBlob Image { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }

        public Artist(string name, string biography = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Biography = biography;
            
            Albums = new List<Album>();
            Tracks = new List<Track>();
        }

        public Artist()
        {
            // Reserved for Serialization
        }
    }
}