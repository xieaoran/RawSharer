using RawSharer.Models.Music;
using RawSharer.Models.Storage;
using System.Data.Entity;

namespace RawSharer.Models
{


    public class DataContext : DbContext
    {
        public DataContext() : base("name=RawSharerData")
        {
            Configuration.LazyLoadingEnabled = false;
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
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<TrackVersion> TrackVersions { get; set; }
        public virtual DbSet<Lyrics> Lyrics { get; set; }
        public virtual DbSet<LyricsSentence> LyricsSentences { get; set; }
        public virtual DbSet<LocalBlob> LocalBlobs { get; set; }
    }
}