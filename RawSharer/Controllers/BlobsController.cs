using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using RawSharer.Models;
using VikingErik.Mvc.ResumingActionResults;

namespace RawSharer.Controllers
{
    public class BlobsController : Controller
    {
        public ActionResult Get(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                var blob = dataContext.LocalBlobsQuery.FirstOrDefault(b => b.Id == id);
                if (blob == null) return HttpNotFound();
                return new ResumingFileStreamResult(blob.GetReadStream(), blob.ContentType);
            }
        }
    }
}