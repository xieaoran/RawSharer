namespace RawSharer.LyricsParser.Settings
{
    public class LrcParserSettings
    {
        public bool MetaDataOnly { get; set; }

        public bool PostProcess { get; set; }

        public LrcParserSettings()
        {
            MetaDataOnly = false;
            PostProcess = true;
        }
    }
}
