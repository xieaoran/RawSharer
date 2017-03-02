using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using RawSharer.Helpers;
using RawSharer.Models.BaseClasses;

namespace RawSharer.Models.Storage
{
    public class LocalBlob : StorageBase
    {
        public string FileName { get; private set; }

        [IgnoreDataMember]
        public string AbsolutePath
            => string.Format(Settings.Storage.PathFormat, Id, FileName);
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