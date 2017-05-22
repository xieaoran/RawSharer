using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using RawSharer.Configs;
using RawSharer.Helpers;

namespace RawSharer.Models.Entities.Storage
{
    public class LocalBlob : StorageBase
    {
        [Required, MaxLength(256)]
        public string FileName { get; set; }

        private string AbsolutePath => Path.Combine(RuntimeConfig.Config.LocalStorage.RootPath,
            Id.ToString(), FileName);

        public override Stream GetReadStream()
        {
            return File.OpenRead(AbsolutePath);
        }

        public override Stream GetWriteStream()
        {
            return File.Open(AbsolutePath, FileMode.CreateNew, FileAccess.Write);
        }

        public LocalBlob(StorageType storagetype, string fileName)
        {
            Id = Guid.NewGuid();
            StorageType = storagetype;
            FileName = fileName;
            ContentType = MimeMapping.GetMimeMapping(fileName);
        }

        public override void HandleUpload(Stream uploadStream)
        {
            var pathRoot = Path.GetDirectoryName(AbsolutePath);
            if (pathRoot == null) return;
            if (!Directory.Exists(pathRoot)) Directory.CreateDirectory(pathRoot);

            Length = uploadStream.Length;

            var writeStream = GetWriteStream();
            uploadStream.CopyTo(writeStream);
            writeStream.Close();

            var readStream = GetReadStream();
            Md5Hash = Md5Helper.GetMd5HashFromStream(readStream);
            readStream.Close();
        }

        public LocalBlob()
        {
            // Reserved for Serialization
        }
    }
}