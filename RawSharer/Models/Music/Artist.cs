using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Music
{
    public class Artist
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string Biography { get; private set; }
        public LocalBlob Image { get; private set; }
        public ICollection<Album> Albums { get; private set; }
        public ICollection<Track> Tracks { get; private set; }

        public Artist(string name, string biography = null, LocalBlob image = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Image = image;
            Biography = biography;
            Albums = new List<Album>();
            Tracks = new List<Track>();
        }

        public Artist()
        {
            // Reserved for DataContext
        }
    }
}