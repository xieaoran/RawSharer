using RawSharer.Models.Music;

namespace RawSharer.ViewModels
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