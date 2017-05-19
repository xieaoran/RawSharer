using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RawSharer.Models.BaseClasses;

namespace RawSharer.Models.Music
{
    public class Genre : Entity
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public Genre(string name)
        {
            Id = Guid.NewGuid();
            Name = name;

            Albums = new List<Album>();
        }

        public Genre()
        {
            // Reserved for Serialization
        }
    }
}