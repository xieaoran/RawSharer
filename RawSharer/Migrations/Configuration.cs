using System;
using System.IO;
using RawSharer.Configs;
using RawSharer.Models;
using RawSharer.Models.Entities.Music;
using RawSharer.Models.Entities.Storage;

namespace RawSharer.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            CreateTriggers(context);
            FillDemoData(context);
        }
        private static void CreateTriggers(DataContext context)
        {
            foreach (var propertyInfo in context.GetType().GetProperties())
            {
                if (!propertyInfo.PropertyType.IsGenericType ||
                    propertyInfo.PropertyType.GetGenericTypeDefinition() != typeof(DbSet<>)) continue;
                var triggerName = $"Trigger_{propertyInfo.Name}";
                var tableName = propertyInfo.Name;
                context.Database.ExecuteSqlCommand(
                    $"IF (OBJECT_ID('{triggerName}', 'TR') IS NOT NULL) " +
                    $"DROP TRIGGER [{triggerName}]");
                context.Database.ExecuteSqlCommand(
                    $"CREATE TRIGGER [{triggerName}] " +
                    $"ON [{tableName}] AFTER INSERT, UPDATE AS " +
                    $"BEGIN UPDATE [{tableName}] SET TimeStamp = GETDATE() END");
            }
        }

        private static void FillDemoData(DataContext context)
        {
            RuntimeConfig.RegisterConfigs();

            var demoAlbumImage = new LocalBlob(StorageType.Image, "cover.jpg");
            context.LocalBlobs.Add(demoAlbumImage);
            using (var uploadStream = File.OpenRead(@"C:\Users\xieaoran\Downloads\folder.jpg"))
            {
                demoAlbumImage.HandleUpload(uploadStream);
            }

            var demoLyricsStorage = new LocalBlob(StorageType.Lyrics, "Demo.lrc");
            context.LocalBlobs.Add(demoLyricsStorage);
            using (var uploadStream = File.OpenRead(@"C:\Users\xieaoran\Downloads\A-Z.lrc"))
            {
                demoLyricsStorage.HandleUpload(uploadStream);
            }

            var demoTrackVersionStorage = new LocalBlob(StorageType.Audio, "Demo.wav");
            context.LocalBlobs.Add(demoTrackVersionStorage);
            using (var uploadStream = File.OpenRead(@"C:\Users\xieaoran\Downloads\A-Z.wav"))
            {
                demoTrackVersionStorage.HandleUpload(uploadStream);
            }

            var animationGenre = new Genre("Animation");
            context.Genres.Add(animationGenre);

            var demoArtist = new Artist("SawanoHiroyuki[nZk]");
            context.Artists.Add(demoArtist);

            var demoAlbum = new Album("A/Z|aLIEz", "2014", 1, 8);
            demoAlbum.Artists.Add(demoArtist);
            demoAlbum.Genre = animationGenre;
            demoAlbum.Image = demoAlbumImage;
            context.Albums.Add(demoAlbum);

            var demoLyrics = new Lyrics(demoLyricsStorage);
            foreach (var sentence in demoLyrics.Parse())
            {
                context.LyricsSentences.Add(sentence);
            }
            context.Lyrics.Add(demoLyrics);

            var demoTrackVersion = new TrackVersion("RawSharer Demo")
            {
                OriginalStorage = demoTrackVersionStorage,
                ConvertedStorage = demoTrackVersionStorage,
                Lyrics = demoLyrics
            };
            context.TrackVersions.Add(demoTrackVersion);

            var demoTrack = new Track("A/Z", 1, 2, TimeSpan.FromSeconds(317.949)) { Album = demoAlbum };
            demoTrack.Artists.Add(demoArtist);
            demoTrack.Versions.Add(demoTrackVersion);
            context.Tracks.Add(demoTrack);
        }
    }
}
