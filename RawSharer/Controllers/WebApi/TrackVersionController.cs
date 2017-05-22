using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RawSharer.Data;
using RawSharer.Shared.Entities.Library;

namespace RawSharer.Controllers.WebApi
{
    public sealed class TrackVersionController : ApiController
    {
        public IEnumerable<TrackVersion> Get()
        {
            using (var dataContext = new LibraryDbContext())
            {
                return dataContext.TrackVersions.Include("OriginalStorage")
                    .Include("ConvertedStorage")
                    .Include("Lyrics")
                    .Include("Lyrics.Sentences")
                    .Include("Track")
                    .Include("Track.Artists")
                    .Include("Track.Album")
                    .Include("Track.Album.Genre")
                    .Include("Track.Album.ImageStorage")
                    .ToArray();
            }
        }

        public string Get(int id)
        {
            return "value";
        }
    }
}
