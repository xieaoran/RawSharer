using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using RawSharer.Configs;
using RawSharer.Models.BaseClasses;

namespace RawSharer.Models.Storage
{
    public class LocalBlob : StorageBase
    {
        public string FileName { get; set; }

        [NotMapped]
        public string AbsolutePath => Path.Combine(RuntimeConfig.Config.LocalStorage.RootPath,
            Id.ToString(), FileName);

        public sealed override Stream GetReadStream()
        {
            return File.OpenRead(AbsolutePath);
        }

        public sealed override Stream GetWriteStream()
        {
            return File.OpenWrite(AbsolutePath);
        }

        public LocalBlob(StorageType storagetype, string fileName, long? length = null)
        {
            Id = Guid.NewGuid();
            StorageType = storagetype;
            FileName = fileName;
            ContentType = MimeMapping.GetMimeMapping(fileName);
            Length = length ?? GetReadStream().Length;
        }

        public LocalBlob()
        {
            // Reserved for Serialization
        }
    }
}