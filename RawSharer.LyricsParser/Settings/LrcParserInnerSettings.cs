namespace RawSharer.LyricsParser.Settings
{
    internal class LrcParserInnerSettings
    {
        public bool MetaDataOnly { get; set; }

        public bool PostProcess { get; set; }

        public LrcParserInnerSettings(LrcParserSettings settings)
        {
            MetaDataOnly = settings.MetaDataOnly;
            PostProcess = !MetaDataOnly && settings.PostProcess;
        }
    }
}
