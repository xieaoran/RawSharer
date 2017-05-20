using System;

namespace RawSharer.LyricsParser.Models
{
    public class ParsedSentence
    {
        /// <summary>
        /// Sequence of Current Sentence, Starting from 0
        /// </summary>
        public int Sequence { get; internal set; }

        /// <summary>
        /// Start Time of Current Sentence
        /// </summary>
        public TimeSpan StartTime { get; internal set; }

        /// <summary>
        /// End Time of Current Sentence
        /// </summary>
        public TimeSpan EndTime { get; internal set; }

        /// <summary>
        /// Duration of Current Sentence
        /// </summary>
        public TimeSpan Duration { get; internal set; }

        /// <summary>
        /// Actual Lyrics Content of Current Sentence
        /// </summary>
        public string Value { get; internal set; }
    }
}
