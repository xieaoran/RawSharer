using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RawSharer.Models.Music
{
    public class Genre
    {
        [Key]
        public Guid Id { get; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public Genre(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Genre()
        {
            // Reserved for Serialization
        }
    }
}