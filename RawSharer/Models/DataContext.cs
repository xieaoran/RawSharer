using System.Data.Entity;
using RawSharer.Models.Entities.Music;
using RawSharer.Models.Entities.Storage;

namespace RawSharer.Models
{
    internal class DataContext : DbContext
    {
        internal DataContext() : base("name=RawSharerData")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
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
            modelBuilder.Entity<Artist>()
                .HasOptional(artist => artist.Image);

            modelBuilder.Entity<Album>()
                .HasMany(album => album.Tracks)
                .WithOptional(track => track.Album);
            modelBuilder.Entity<Album>()
                .HasOptional(album => album.Image);

            modelBuilder.Entity<Track>()
                .HasMany(track => track.Versions)
                .WithOptional(version => version.Track);

            modelBuilder.Entity<TrackVersion>()
                .HasOptional(version => version.Lyrics)
                .WithOptionalPrincipal(lyrics => lyrics.TrackVersion);
            modelBuilder.Entity<TrackVersion>()
                .HasOptional(version => version.OriginalStorage);
            modelBuilder.Entity<TrackVersion>()
                .HasOptional(version => version.ConvertedStorage);

            modelBuilder.Entity<Lyrics>()
                .HasMany(lyrics => lyrics.Sentences)
                .WithOptional(lyricsSentence => lyricsSentence.Lyrics);

            base.OnModelCreating(modelBuilder);
        }
        internal virtual DbSet<Album> Albums { get; set; }
        internal virtual DbSet<Artist> Artists { get; set; }
        internal virtual DbSet<Genre> Genres { get; set; }
        internal virtual DbSet<Track> Tracks { get; set; }
        internal virtual DbSet<TrackVersion> TrackVersions { get; set; }
        internal virtual DbSet<Lyrics> Lyrics { get; set; }
        internal virtual DbSet<LyricsSentence> LyricsSentences { get; set; }
        internal virtual DbSet<LocalBlob> LocalBlobs { get; set; }
    }
}