using System;
using System.Web.Mvc;
using RawSharer.Models;
using VikingErik.Mvc.ResumingActionResults;

namespace RawSharer.Controllers.Core
{
    public sealed class BlobsController : Controller
    {
        public ActionResult Get(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                var blob = dataContext.LocalBlobs.Find(id);
                if (blob == null) return HttpNotFound();
                return new ResumingFileStreamResult(blob.GetReadStream(), blob.ContentType);
            }
        }
    }
}