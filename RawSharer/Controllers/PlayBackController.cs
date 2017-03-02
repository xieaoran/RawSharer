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
        public ActionResult Track(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                var track = dataContext.Tracks.FirstOrDefault(t => t.Id == id);
                if (track == null) return HttpNotFound();
                return View("PlayBack", new PlayBackViewModel(track));
            }
        }
    }
}