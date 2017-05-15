using System.Collections.Generic;
using RawSharer.Models.BaseClasses;
using RawSharer.Models.Music;
using RawSharer.Models.Storage;

namespace RawSharer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RawSharer.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RawSharer.Models.DataContext context)
        {
            //var animationGenre = new Genre("Animation");
            //context.Genres.AddOrUpdate(genre => genre.Name,
            //    animationGenre);
            //var demoArtist = new Artist("RADWIMPS");
            //context.Artists.AddOrUpdate(artist => artist.Name, demoArtist);
            //var demoAlbumImage = new LocalBlob(StorageType.Image, "cover.jpg", 928441);
            //context.LocalBlobs.AddOrUpdate(blob => blob.FileName, demoAlbumImage);
            //var demoAlbum = new Album("¾ý¤ÎÃû¤Ï¡£",
            //    new List<Artist> { demoArtist }, animationGenre, "2016", 1, 27, demoAlbumImage);
            //context.Albums.AddOrUpdate(album => album.Name, demoAlbum);
            //var demoTrackFile = new LocalBlob(StorageType.Audio, "Demo.wav", 71131500);
            //context.LocalBlobs.AddOrUpdate(blob => blob.FileName, demoTrackFile);
            //var demoLyrics = new LocalBlob(StorageType.Lyrics, "Demo.lrc", 233);
            //context.LocalBlobs.AddOrUpdate(blob => blob.FileName, demoLyrics);
            //var demoTrack = new Track(demoTrackFile, demoTrackFile, "¥Ç©`¥È", new List<Album> { demoAlbum },
            //    new List<Artist> { demoArtist }, 1, 10, TimeSpan.FromSeconds(243.755), demoLyrics);
            //context.Tracks.AddOrUpdate(track => track.Name, demoTrack);
        }
    }
}
