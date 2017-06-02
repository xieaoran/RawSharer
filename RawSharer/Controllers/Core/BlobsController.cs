using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using RawSharer.Data;
using VikingErik.Mvc.ResumingActionResults;

namespace RawSharer.Controllers.Core
{
    public sealed class BlobsController : Controller
    {
        public async Task<ActionResult> Get(Guid id)
        {
            using (var dataContext = new LibraryDbContext())
            {
                var blob = await dataContext.BlobStorages.FindAsync(id);
                if (blob == null) return HttpNotFound();
                return new ResumingFileStreamResult(blob.GetReadStream(), blob.MimeType);
            }
        }
    }
}