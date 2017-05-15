using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using RawSharer.Helpers;

namespace RawSharer.Models.BaseClasses
{
    public enum StorageType
    {
        Audio,
        Video,
        Image,
        Lyrics,
        Other
    }
    public abstract class StorageBase
    {
        [Key]
        public Guid Id { get; protected set; }
        public StorageType StorageType { get; protected set; }
        public string ContentType { get; protected set; }
        public string Md5Hash { get; protected set; }
        public long Length { get; protected set; }

        public abstract Stream GetReadStream();
        public abstract Stream GetWriteStream();
        public string GetMd5Hash()
        {
            string md5;
            using (var readStream = GetReadStream())
            {
                md5 = Md5Helper.GetMd5HashFromStream(readStream);
            }
            return md5;
        }
    }
}