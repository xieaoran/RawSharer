﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RawSharer.Models.Entities.Base;

namespace RawSharer.Models.Entities.Music
{
    public class Genre : Entity
    {
        [Required]
        [MaxLength(128)]
        [Index(IsClustered = false, IsUnique = false)]
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