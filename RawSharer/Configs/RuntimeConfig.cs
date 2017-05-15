using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace RawSharer.Configs
{
    public static class RuntimeConfig
    {
        public static RawSharerSection Config { get; private set; }

        private static void Initialize()
        {
            Config = new RawSharerSection(true);
            var writableConfig = WebConfigurationManager.OpenWebConfiguration("~");
            writableConfig.Sections.Add("rawsharer", Config);
            writableConfig.Save();
        }

        public static void RegisterConfigs()
        {
            Config = (RawSharerSection)WebConfigurationManager.GetSection("rawsharer");
            if (Config == null) Initialize();
        }
    }
}