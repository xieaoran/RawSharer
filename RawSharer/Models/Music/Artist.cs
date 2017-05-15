using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Music
{
    public class Artist
    {
        [Key]
        public Guid Id { get; }
        [Required]
        public string Name { get; set; }
        public string Biography { get; set; }

        public virtual LocalBlob Image { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }

        public Artist(string name, string biography = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Biography = biography;
        }

        public Artist()
        {
            // Reserved for Serialization
        }
    }
}