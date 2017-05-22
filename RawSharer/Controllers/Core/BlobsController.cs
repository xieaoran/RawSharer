using System;
using System.Web.Mvc;
using RawSharer.Data;
using VikingErik.Mvc.ResumingActionResults;

namespace RawSharer.Controllers.Core
{
    public sealed class BlobsController : Controller
    {
        public ActionResult Get(Guid id)
        {
            using (var dataContext = new LibraryDbContext())
            {
                var blob = dataContext.BlobStorages.Find(id);
                if (blob == null) return HttpNotFound();
                return new ResumingFileStreamResult(blob.GetReadStream(), blob.MimeType);
            }
        }
    }
}