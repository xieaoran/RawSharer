using System.IO;
using System.Threading.Tasks;
using RawSharer.Shared.Entities.Storage;

namespace RawSharer.Shared.Contracts.Extension
{
    public interface IStorageExtension
    {
        Stream GetReadStream(BlobStorage blob);
        Stream GetWriteStream(BlobStorage blob);
        Task<BlobStorage> HandleUploadAsync();
    }
}
