using System.Collections.Generic;
using RawSharer.Lyrics.Models;

namespace RawSharer.Lyrics.Utils
{
    internal class LyricsSentenceComparer : IComparer<LyricsSentence>
    {
        public int Compare(LyricsSentence x, LyricsSentence y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.StartTime.CompareTo(y.StartTime);
        }
    }
}
