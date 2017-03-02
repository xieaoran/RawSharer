using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RawSharer.Models.Music;
using RawSharer.Models.Storage;

namespace RawSharer.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataContext : DbContext
    {
        public DataContext() : base("name=RawSharerData")
        {
            Configuration.LazyLoadingEnabled = false;
            LoadData();
        }

        public void LoadData()
        {
            LocalBlobs.Load();
            Genres.Load();
            Artists.Include("Image")
                .Load();
            Albums.Include("Artists")
                .Include("Genre")
                .Include("Image")
                .Load();
            Tracks.Include("Albums")
                .Include("Artists")
                .Include("OriginalStorage")
                .Include("ConvertedStorage")
                .Include("Lyrics")
                .Load();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(genre => genre.Albums)
                .WithOptional(album => album.Genre);
            modelBuilder.Entity<Artist>()
                .HasMany(artist => artist.Albums)
                .WithMany(album => album.Artists)
                .Map(
                    map => map.MapLeftKey("ArtistId")
                        .MapRightKey("AlbumId")
                        .ToTable("Artist_Album"));
            modelBuilder.Entity<Artist>()
                .HasMany(artist => artist.Tracks)
                .WithMany(track => track.Artists)
                .Map(
                    map => map.MapLeftKey("ArtistId")
                        .MapRightKey("TrackId")
                        .ToTable("Artist_Track"));
            modelBuilder.Entity<Album>()
                .HasMany(album => album.Tracks)
                .WithMany(track => track.Albums)
                .Map(
                    map => map.MapLeftKey("AlbumId")
                        .MapRightKey("TrackId")
                        .ToTable("Album_Track"));
            modelBuilder.Entity<Artist>()
                .HasOptional(artist => artist.Image);
            modelBuilder.Entity<Album>()
                .HasOptional(album => album.Image);
            modelBuilder.Entity<Track>()
                .HasOptional(track => track.OriginalStorage);
            modelBuilder.Entity<Track>()
                .HasOptional(track => track.ConvertedStorage);
            modelBuilder.Entity<Track>()
                .HasOptional(track => track.Lyrics);
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<LocalBlob> LocalBlobs { get; set; }
    }
}