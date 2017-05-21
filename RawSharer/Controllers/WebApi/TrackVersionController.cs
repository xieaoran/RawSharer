using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RawSharer.Models;
using RawSharer.Models.Entities.Music;

namespace RawSharer.Controllers.WebApi
{
    public class TrackVersionController : ApiController
    {
        public IEnumerable<TrackVersion> Get()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.TrackVersions.Include("OriginalStorage")
                    .Include("ConvertedStorage")
                    .Include("Lyrics")
                    .Include("Lyrics.Sentences")
                    .Include("Track")
                    .Include("Track.Artists")
                    .Include("Track.Album")
                    .Include("Track.Album.Genre")
                    .Include("Track.Album.Image")
                    .ToArray();
            }
        }

        public string Get(int id)
        {
            return "value";
        }
    }
}
