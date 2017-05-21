using RawSharer.Models.Entities.Music;

namespace RawSharer.ViewModels.UI
{
    public class PlayBackViewModel
    {
        public TrackVersion TrackVersion { get; private set; }

        public PlayBackViewModel(TrackVersion trackVersion)
        {
            TrackVersion = trackVersion;
        }
    }
}