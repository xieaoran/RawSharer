using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RawSharer.LyricsParser.Parsers;
using RawSharer.Models.Entities.Base;
using RawSharer.Models.Entities.Storage;

namespace RawSharer.Models.Entities.Music
{
    public class Lyrics : Entity
    {
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
            Sentences = new List<LyricsSentence>();
        }

        public IEnumerable<LyricsSentence> Parse()
        {
            var readStream = LyricsStorage.GetReadStream();
            var parsedLyrics = LrcParser.Parse(readStream);
            readStream.Close();

            Album = parsedLyrics.Album;
            Artist = parsedLyrics.Artist;
            Title = parsedLyrics.Title;
            Author = parsedLyrics.Author;
            LrcCreator = parsedLyrics.LrcCreator;
            Length = parsedLyrics.Sentences.Count;

            var sentences = parsedLyrics.Sentences.Select(
                s => new LyricsSentence(s.Sequence, s.StartTime, s.EndTime, s.Duration, s.Value)).ToArray();
            foreach (var sentence in sentences)
            {
                Sentences.Add(sentence);
            }
            return sentences;
        }

        public Lyrics()
        {
            // Reserved for Serialization
        }
    }
}