using System.Collections.Generic;

namespace RawSharer.LyricsParser.Models
{
    public class ParsedLyrics
    {
        /// <summary>
        /// [al:Album where the song is from]
        /// </summary>
        public string Album { get; internal set; }

        /// <summary>
        /// [ar:Lyrics artist]
        /// </summary>
        public string Artist { get; internal set; }

        /// <summary>
        /// [ti:Lyrics (song) title]
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// [au:Creator of the Songtext]
        /// </summary>
        public string Author { get; internal set; }

        /// <summary>
        /// [by:Creator of the LRC file]
        /// </summary>
        public string LrcCreator { get; internal set; }

        /// <summary>
        /// [offset:+/- Overall timestamp adjustment in milliseconds, + shifts time up, - shifts down]
        /// </summary>
        public int OffsetMs { get; internal set; }

        /// <summary>
        /// Sentences of Lyrics
        /// </summary>
        public List<ParsedSentence> Sentences { get; internal set; }

        public ParsedLyrics()
        {
            Sentences = new List<ParsedSentence>();
        }
    }
}
