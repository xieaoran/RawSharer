using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RawSharer.Models;
using RawSharer.Models.Music;

namespace RawSharer.ViewModels
{
    public class PlayBackViewModel
    {
        public Track Track { get; private set; }
        public Album SenderAlbum { get; private set; }

        public PlayBackViewModel(Track track, Album senderAlbum = null)
        {
            Track = track;
            SenderAlbum = senderAlbum ?? track.Albums.FirstOrDefault();
        }
    }
}