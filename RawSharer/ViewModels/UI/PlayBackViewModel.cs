using RawSharer.Models.Entities.Music;

namespace RawSharer.ViewModels.UI
{
    internal sealed class PlayBackViewModel
    {
        internal TrackVersion TrackVersion { get; }

        internal PlayBackViewModel(TrackVersion trackVersion)
        {
            TrackVersion = trackVersion;
        }
    }
}