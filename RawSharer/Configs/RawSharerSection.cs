using System.Configuration;

namespace RawSharer.Configs
{
    public sealed class RawSharerSection : ConfigurationSection
    {
        [ConfigurationProperty("format")]
        public FormatElement Format
        {
            get => (FormatElement) base["format"];
            set => base["format"] = value;
        }

        public RawSharerSection(bool createElements)
        {
            if (!createElements) return;
            Format = new FormatElement();
        }

        public RawSharerSection()
        {
            // Reserved for Serialization
        }
    }
}