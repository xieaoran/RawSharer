using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using RawSharer.Data;
using RawSharer.Shared.Entities.Library;

namespace RawSharer.Controllers.WebApi
{
    public sealed class TrackVersionController : ApiController
    {
        public async Task<IEnumerable<TrackVersion>> Get()
        {
            using (var dataContext = new LibraryDbContext())
            {
                return await dataContext.TrackVersions.Include("OriginalStorage")
                    .Include("ConvertedStorage")
                    .Include("Lyrics")
                    .Include("Lyrics.Sentences")
                    .Include("Track")
                    .Include("Track.Artists")
                    .Include("Track.Album")
                    .Include("Track.Album.Genre")
                    .Include("Track.Album.ImageStorage")
                    .ToArrayAsync();
            }
        }

        public string Get(int id)
        {
            return "value";
        }
    }
}
