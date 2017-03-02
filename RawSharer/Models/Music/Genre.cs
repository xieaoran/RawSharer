using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RawSharer.Models.Music
{
    public class Genre
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public string Name { get; private set; }

        public ICollection<Album> Albums { get; private set; }

        public Genre(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Albums = new List<Album>();
        }

        public Genre()
        {
            // Reserved for DataContext
        }
    }
}