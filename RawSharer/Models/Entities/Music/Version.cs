using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Music
{
    public class Version
    {
        [Key]
        public Guid Id { get; }

        [Required]
        public virtual LocalBlob OriginalStorage { get; set; }
        [Required]
        public virtual LocalBlob ConvertedStorage { get; set; }

        public Version()
        {
            Id = Guid.NewGuid();
        }

    }
}