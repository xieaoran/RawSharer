using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RawSharer.Models;
using RawSharer.ViewModels;

namespace RawSharer.Controllers
{
    public class PlayBackController : Controller
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