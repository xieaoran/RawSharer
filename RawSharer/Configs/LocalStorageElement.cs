using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RawSharer.Configs
{
    public sealed class LocalStorageElement : ConfigurationElement
    {
        [ConfigurationProperty("rootPath",
            DefaultValue = @"C:\LocalWorkSpace\RawSharer\RawSharer\Storage")]
        public string RootPath
        {
            get => (string) base["rootPath"];
            set => base["rootPath"] = value;
        }
    }
}