using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RawSharer.Models.Storage;

namespace RawSharer.Models.Music
{
    public class Album
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public ICollection<Artist> Artists { get; private set; }
        public ICollection<Track> Tracks { get; private set; }
        public Genre Genre { get; private set; }
        public string ReleaseDate { get; private set; }
        public byte? DiskCount { get; private set; }
        public byte? TrackCount { get; private set; }
        public LocalBlob Image { get; private set; }

        public Album(string name,
            ICollection<Artist> artists = null, Genre genre = null,
            string releaseDate = null, byte? diskCount = null,
            byte? trackCount = null, LocalBlob image = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            if (artists == null)
            {
                Artists = new List<Artist>();
            }
            else
            {
                Artists = artists;
                foreach (var artist in artists)
                {
                    artist.Albums.Add(this);
                }
            }
            Tracks = new List<Track>();
            genre?.Albums.Add(this);
            Genre = genre;
            ReleaseDate = releaseDate;
            DiskCount = diskCount;
            TrackCount = trackCount;
            Image = image;
        }

        public string GetArtists()
        {
            return string.Join(Settings.UiStrings.ArtistSeparator, Artists.Select(artist => artist.Name));
        }

        public Album()
        {
            // Reserved for DataContext
        }
    }
}