using System;
using System.ComponentModel.DataAnnotations;
using RawSharer.Shared.Entities.Storage;

namespace RawSharer.Shared.Entities.Library
{
    public class Lyrics : EntityBase
    {
        [Required]
        public int Length { get; set; }

        public string Album { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string LrcCreator { get; set; }

        public virtual BlobStorage LyricsStorage { get; set; }
        public virtual TrackVersion TrackVersion { get; set; }

        public Lyrics(BlobStorage lyricsStorage)
        {
            Id = Guid.NewGuid();
            Length = 0;

            LyricsStorage = lyricsStorage;
        }

        public Lyrics()
        {
            // Reserved for Serialization
        }
    }
}