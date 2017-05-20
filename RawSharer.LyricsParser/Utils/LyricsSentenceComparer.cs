using System.Collections.Generic;
using RawSharer.LyricsParser.Models;

namespace RawSharer.LyricsParser.Utils
{
    internal class LyricsSentenceComparer : IComparer<ParsedSentence>
    {
        public int Compare(ParsedSentence x, ParsedSentence y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.StartTime.CompareTo(y.StartTime);
        }
    }
}
