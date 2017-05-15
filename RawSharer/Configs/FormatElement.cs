using System.Configuration;

namespace RawSharer.Configs
{
    public sealed class FormatElement : ConfigurationElement
    {
        [ConfigurationProperty("artistSeparator", DefaultValue = "/")]
        public string ArtistSeparator
        {
            get => (string)base["artistSeparator"];
            set => base["artistSeparator"] = value;
        }
    }
}