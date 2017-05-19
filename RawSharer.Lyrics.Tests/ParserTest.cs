using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RawSharer.Lyrics.Tests
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parse()
        {
            var fileStream = File.OpenRead(@"C:\Users\xieaoran\Downloads\test.lrc");
            var result = Parsers.LrcParser.Parse(fileStream);
            Assert.IsNotNull(result);
        }
    }
}
