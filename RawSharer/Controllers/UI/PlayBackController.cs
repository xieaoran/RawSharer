using System;
using System.Linq;
using System.Web.Mvc;
using RawSharer.Models;
using RawSharer.ViewModels.UI;

namespace RawSharer.Controllers.UI
{
    public sealed class PlayBackController : Controller
    {
        public ActionResult TrackVersion(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                var trackVersion = dataContext.TrackVersions
                    .Include("OriginalStorage")
                    .Include("ConvertedStorage")
                    .Include("Lyrics")
                    .Include("Lyrics.Sentences")
                    .Include("Track")
                    .Include("Track.Artists")
                    .Include("Track.Album")
                    .Include("Track.Album.Genre")
                    .Include("Track.Album.Image")
                    .FirstOrDefault(t => t.Id == id);
                if (trackVersion == null) return HttpNotFound();
                return View("PlayBack", new PlayBackViewModel(trackVersion));
            }
        }
    }
}