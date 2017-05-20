using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Timers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawSharer.LyricsParser.Models;

namespace RawSharer.LyricsParser.Tests
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ParseString()
        {
            var lrcContent = "[00:01.00]A/Z\n" +
                             "[00:05.00]「Aldnoah Zero ED1」\n" +
                             "[00:09.00] 歌:SawanoHiroyuki[nZk]\n" +
                             "[00:13.00]\n" +
                             "[00:15.00]\n" +
                             "[00:17.39]0　始(は)まりにも 终(お)わりにも 変(か)わる光\n" +
                             "[00:25.31] 下(した)に向(む)けたまま かざすのを止(や)めた あの时(とき)ALD\n";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(lrcContent));
            var lyrics = Parsers.LrcParser.Parse(stream);
            Assert.IsNotNull(lyrics);
        }

        [TestMethod]
        public void PerformanceTest()
        {
            const string lrcPath = @"C:\Users\xieaoran\Downloads\A-Z.lrc";
            const int parseTimes = 100000;

            var lrcFile = File.OpenRead(lrcPath);
            var reader = new StreamReader(lrcFile);
            var lrcContent = reader.ReadToEnd();
            ParsedLyrics lyrics = null;

            var timer = new Stopwatch();
            timer.Start();
            for (var i = 0; i < parseTimes; i++)
            {
                lyrics = Parsers.LrcParser.Parse(lrcContent);
            }
            timer.Stop();
            Assert.IsNotNull(lyrics);
            Debug.Print($"Lyrics Length: {lyrics.Sentences.Count} Sentences | " +
                        $"{lrcContent.Length} Characters\n");
            Debug.Print($"Parsing {parseTimes} Times in {timer.ElapsedMilliseconds} ms.");
        }
    }
}
