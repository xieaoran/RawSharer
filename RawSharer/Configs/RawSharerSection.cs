using System.Configuration;

namespace RawSharer.Configs
{
    public sealed class RawSharerSection : ConfigurationSection
    {
        [ConfigurationProperty("localStorage")]
        public LocalStorageElement LocalStorage
        {
            get => (LocalStorageElement)base["localStorage"];
            set => base["localStorage"] = value;
        }

        [ConfigurationProperty("format")]
        public FormatElement Format
        {
            get => (FormatElement) base["format"];
            set => base["format"] = value;
        }

        public RawSharerSection(bool createElements)
        {
            if (!createElements) return;
            LocalStorage = new LocalStorageElement();
            Format = new FormatElement();
        }

        public RawSharerSection()
        {
            // Reserved for Serialization
        }
    }
}