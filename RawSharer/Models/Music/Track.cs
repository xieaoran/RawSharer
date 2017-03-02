using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Music
{
    public class Track
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public LocalBlob OriginalStorage { get; private set; }
        [Required]
        public LocalBlob ConvertedStorage { get; private set; }
        [Required]
        public string Name { get; private set; }
        public ICollection<Album> Albums { get; private set; }
        public ICollection<Artist> Artists { get; private set; }
        public byte? DiskNumber { get; private set; }
        public byte? TrackNumber { get; private set; }
        public TimeSpan? Duration { get; private set; }
        public LocalBlob Lyrics { get; private set; }
        public Track(LocalBlob originalStorage, LocalBlob convertedStorage, 
            string name, ICollection<Album> albums = null,
            ICollection<Artist> artists = null, byte? diskNumber = null,
            byte? trackNumber = null, TimeSpan? duration = null, LocalBlob lyrics = null)
        {
            Id = Guid.NewGuid();
            OriginalStorage = originalStorage;
            ConvertedStorage = convertedStorage;
            Name = name;
            if (albums == null)
            {
                Albums = new List<Album>();
            }
            else
            {
                Albums = albums;
                foreach (var album in albums)
                {
                    album.Tracks.Add(this);
                }
            }
            if (artists == null)
            {
                Albums = new List<Album>();
            }
            else
            {
                Artists = artists;
                foreach (var artist in artists)
                {
                    artist.Tracks.Add(this);
                }
            }
            DiskNumber = diskNumber;
            TrackNumber = trackNumber;
            Duration = duration;
            Lyrics = lyrics;
        }

        public Track()
        {
            // Reserved for DataContext
        }
        public string GetArtists()
        {
            return string.Join(Settings.UiStrings.ArtistSeparator, Artists.Select(artist => artist.Name));
        }
    }
}