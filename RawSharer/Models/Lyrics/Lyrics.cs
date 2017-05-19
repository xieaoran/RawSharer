using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using RawSharer.Models.BaseClasses;
using RawSharer.Models.Music;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Lyrics
{
    public class Lyrics : Entity
    {
        [Required]
        public string RawContent { get; set; }
        [Required]
        public int Length { get; set; }

        public string Album { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string LrcCreator { get; set; }

        public virtual LocalBlob LyricsStorage { get; set; }
        public virtual TrackVersion TrackVersion { get; set; }
        public virtual ICollection<LyricsSentence> Sentences { get; set; }

        public Lyrics(LocalBlob lyricsStorage)
        {
            Id = Guid.NewGuid();
            Length = 0;

            LyricsStorage = lyricsStorage;
            FetchRawContent();
        }

        private void FetchRawContent()
        {
            var readStream = LyricsStorage.GetReadStream();
            var reader = new StreamReader(readStream);
            RawContent = reader.ReadToEnd();
            reader.Close();
            readStream.Close();
        }

        public Lyrics()
        {
            // Reserved for Serialization
        }
    }
}