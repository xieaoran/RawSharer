using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RawSharer.Helpers
{
    public static class Md5Helper
    {
        public static string GetMd5HashFromStream(Stream stream)
        {
            var md5Builder = new StringBuilder(32);
            var md5Provider = new MD5CryptoServiceProvider();
            var md5 = md5Provider.ComputeHash(stream);
            foreach (var md5Byte in md5)
            {
                md5Builder.Append(md5Byte.ToString("x").PadLeft(2, '0'));
            }
            return md5Builder.ToString();
        }
    }
}