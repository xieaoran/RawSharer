using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var result = Parsers.LrcParser.Parse(stream);
        }
    }
}
