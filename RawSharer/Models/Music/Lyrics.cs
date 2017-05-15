using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RawSharer.Models.Music
{
    public class Lyrics
    {
        [Key]
        public Guid Id { get; }

    }
}