#nullable disable

using NUnit.Framework;
using SixLabors.ImageSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class LiveApiTests : ApiTestBase
    {
        internal Guid ReleaseId { get; } = Guid.Parse("75c1a74c-27bc-4ef6-884b-0b56515ea6e0");
        internal Guid TrackId { get; } = Guid.Parse("f2db30c8-1547-4c41-93d9-dca2bc822cac");

        internal Guid? PlaylistId { get; private set; }
        internal Guid? UserId { get; private set; }

        [Test, Order(1)]
        public async Task Test_Login()
        {
            await Api.Login(Credentials);

            Assert.IsTrue(IsLoggedIn);
        }

        [Test, Order(2)]
        public async Task Test_GetSelf()
        {
            var self = await Api.GetSelf();

            Assert.IsNotNull(self);
            Assert.AreEqual(Credentials.Email, self.User.Email);
            Assert.IsTrue(self.User.HasGold, "The test account should have an active gold subscription, otherwise some tests are bound to fail.");

            UserId = self.User.Id;
        }

        [Test, Order(3)]
        public async Task Test_GetTrackSearchFilters()
        {
            var filters = await Api.GetTrackSearchFilters();

            Assert.IsNotNull(filters);
            Assert.IsTrue(filters.Genres.Length > 0);
            Assert.IsTrue(filters.Tags.Length > 0);
            Assert.IsTrue(filters.Types.Length > 0);
        }

        [Test, Order(4)]
        public async Task Test_SearchTracks()
        {
            var tracks = await Api.SearchTracks(new TrackSearchRequest()
            {
                Limit = 1,
                Skip = 0,
                Creatorfriendly = true,
                ReleaseTypes = new[] { "EP" },
                Tags = new[] { "silkinitialbulkimport" },
            });

            Assert.IsNotNull(tracks);
            Assert.IsTrue(tracks.Results.Length >= 1);
            Assert.AreEqual(Guid.Parse("{65c9d857-4f34-4ad7-925c-fefb92e4d36d}"), tracks.Results[0].Id);

            Assert.IsNotNull(tracks.Results[0].Artists);
            Assert.IsNotNull(tracks.Results[0].ArtistsTitle);

            Assert.IsNotNull(tracks.Results[0].Artists[0]);

            Assert.AreNotEqual(Guid.Empty, tracks.Results[0].Artists[0].Id);
            Assert.AreNotEqual(Guid.Empty, tracks.Results[0].Artists[0].ProfileFileId);
            Assert.AreNotEqual(Guid.Empty, tracks.Results[0].Artists[0].CatalogRecordId);

            Assert.AreNotEqual(string.Empty, tracks.Results[0].Artists[0].Name);
            Assert.AreNotEqual(string.Empty, tracks.Results[0].Artists[0].Role);
            Assert.AreNotEqual(string.Empty, tracks.Results[0].Artists[0].Uri);
        }

        [Test, Order(5)]
        public async Task Test_GetReleases()
        {
            var releases = await Api.GetReleases(new ReleaseBrowseRequest()
            {
                Limit = 1,
                Skip = 0
            });

            Assert.IsNotNull(releases);
            Assert.IsTrue(releases.Results.Data.Length == 1);
            Assert.IsNotNull(releases.Results.Data[0]);
        }

        [Test, Order(6)]
        public async Task Test_GetRelease()
        {
            var release = await Api.GetRelease("MCRLX001-8");

            Assert.IsNotNull(release);
            Assert.IsNotNull(release.Release);
            Assert.IsNotNull(release.Tracks);
            Assert.IsTrue(release.Tracks.Length == 1);
        }

        // requires active gold subscription
        [Test, Order(7)]
        public async Task Test_DownloadTrackAsByteArray()
        {
            var release = await Api.DownloadTrackAsByteArray(new TrackDownloadRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.IsNotNull(release);
            Assert.IsTrue(release.Length > 0);
        }

        // requires active gold subscription
        [Test, Order(8)]
        public async Task Test_DownloadTrackAsStream()
        {
            var release = await Api.DownloadTrackAsStream(new TrackDownloadRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.IsNotNull(release);

            var result = release.ToByteArray();
            Assert.IsTrue(result.Length > 0);
        }

        [Test, Order(9)]
        public async Task Test_StreamTrackAsStream()
        {
            var release = await Api.StreamTrackAsStream(new TrackStreamRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.IsNotNull(release);

            var result = release.ToByteArray();
            Assert.IsTrue(result.Length > 0);
        }

        [Test, Order(10)]
        public async Task Test_CreatePlaylist()
        {
            var response = await Api.CreatePlaylist(new PlaylistCreateRequest()
            {
                Title = "MyTestPlaylist",
            });

            Assert.IsNotNull(response);

            PlaylistId = response.Id;
        }

        [Test, Order(11)]
        public async Task Test_PlaylistAddTrack()
        {
            if (PlaylistId is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            await Api.PlaylistAddTrack(PlaylistId.Value, new PlaylistAddTrackRequest()
            {
                Records = new[]
                {
                    new PlaylistRecord()
                    {
                        PlaylistId = PlaylistId.Value,
                        ReleaseId = ReleaseId,
                        TrackId = TrackId,
                    }
                }
            });
        }

        [Test, Order(12)]
        public async Task Test_PlaylistDeleteTrack()
        {
            if (PlaylistId is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            await Api.PlaylistDeleteTrack(PlaylistId.Value, new PlaylistDeleteTrackRequest()
            {
                Records = new[]
                {
                    new PlaylistRecord()
                    {
                        PlaylistId = PlaylistId.Value,
                        ReleaseId = ReleaseId,
                        TrackId = TrackId,
                    }
                }
            });
        }

        [Test, Order(13)]
        public async Task Test_GetSelfPlaylists()
        {
            var playlists = await Api.GetSelfPlaylists();

            Assert.IsNotNull(playlists);

            Assert.IsTrue(playlists.Playlists.Data.Length >= 1);
            Assert.IsTrue(playlists.Playlists.Data.Any(p => p.Id == PlaylistId));
        }

        [Test, Order(14)]
        public async Task Test_GetPlaylist()
        {
            if (PlaylistId is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            var playlist = await Api.GetPlaylist(PlaylistId.Value);

            Assert.IsNotNull(playlist);
        }

        [Test, Order(15)]
        public async Task Test_UpdatePlaylist()
        {
            if (PlaylistId is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            var playlist = await Api.UpdatePlaylist(new UpdatePlaylistRequest()
            {
                Title = "MyRenameTestPlaylist",
                PlaylistId = PlaylistId.Value,
                UserId = UserId.Value,
            });

            Assert.AreEqual("MyRenameTestPlaylist", playlist.Title);
        }

        [Test, Order(16)]
        public async Task Test_MakePlaylistPublic()
        {
            if (PlaylistId is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            var playlist = await Api.UpdatePlaylist(new UpdatePlaylistRequest()
            {
                Title = null,
                PlaylistId = PlaylistId.Value,
                UserId = UserId.Value,
                IsPublic = true,
            });

            Assert.AreEqual(true, playlist.IsPublic);
        }

        [Test, Order(17)]
        public async Task Test_MakePlaylistPrivate()
        {
            if (PlaylistId is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            var playlist = await Api.UpdatePlaylist(new UpdatePlaylistRequest()
            {
                Title = null,
                PlaylistId = PlaylistId.Value,
                UserId = UserId.Value,
                IsPublic = false,
            });

            Assert.AreEqual(false, playlist.IsPublic);
        }

        [Test, Order(18)]
        public async Task Test_DeletePlaylist()
        {
            if (PlaylistId is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            await Api.DeletePlaylist(PlaylistId.Value);
        }

        [Test, Order(19)]
        public async Task Test_DownloadArtistPhoto_WithHugePhoto()
        {
            var builder = ArtistPhotoBuilder.Create(new Artist()
            {
                ArtistId = Guid.Parse("{4f2c83b1-7a08-42df-bf1c-d1341b8982ae}"),
                Uri = "aftruu",
            }).WithHugePhoto();

            using (var stream = await Cdn.GetArtistPhotoAsStream(builder))
            {
                using (var image = Image.Load(stream))
                {
                    Assert.GreaterOrEqual(image.Height, 3000);
                    Assert.GreaterOrEqual(image.Width, 3000);
                }
            }
        }

        [Test, Order(20)]
        public async Task Test_DownloadArtistPhoto_WithLargePhoto()
        {
            var builder = ArtistPhotoBuilder.Create(new Artist()
            {
                ArtistId = Guid.Parse("{4f2c83b1-7a08-42df-bf1c-d1341b8982ae}"),
                Uri = "aftruu",
            }).WithLargePhoto();

            using (var stream = await Cdn.GetArtistPhotoAsStream(builder))
            {
                using (var image = Image.Load(stream))
                {
                    Assert.GreaterOrEqual(image.Height, 1024);
                    Assert.GreaterOrEqual(image.Width, 1024);
                }
            }
        }

        [Test, Order(21)]
        public async Task Test_DownloadArtistPhoto_WithSmallPhoto()
        {
            var builder = ArtistPhotoBuilder.Create(new Artist()
            {
                ArtistId = Guid.Parse("{4f2c83b1-7a08-42df-bf1c-d1341b8982ae}"),
                Uri = "aftruu",
            }).WithSmallPhoto();

            using (var stream = await Cdn.GetArtistPhotoAsStream(builder))
            {
                using (var image = Image.Load(stream))
                {
                    Assert.GreaterOrEqual(image.Height, 256);
                    Assert.GreaterOrEqual(image.Width, 256);
                }
            }
        }

        [Test, Order(22)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithHugeCoverArt()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithHugeCoverArt();

            var bytes = await Cdn.GetReleaseCoverAsByteArray(builder);

            using (var stream = new MemoryStream(bytes))
            using (var image = Image.Load(stream))
            {
                Assert.GreaterOrEqual(image.Height, 3000);
                Assert.GreaterOrEqual(image.Width, 3000);
            }
        }

        [Test, Order(23)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithLargeCoverArt()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithLargeCoverArt();

            var bytes = await Cdn.GetReleaseCoverAsByteArray(builder);

            using (var stream = new MemoryStream(bytes))
            using (var image = Image.Load(stream))
            {
                Assert.GreaterOrEqual(image.Height, 1024);
                Assert.GreaterOrEqual(image.Width, 1024);
            }
        }

        [Test, Order(24)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithMediumCoverArt()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithMediumCoverArt();

            var bytes = await Cdn.GetReleaseCoverAsByteArray(builder);

            using (var stream = new MemoryStream(bytes))
            using (var image = Image.Load(stream))
            {
                Assert.GreaterOrEqual(image.Height, 600);
                Assert.GreaterOrEqual(image.Width, 600);
            }
        }

        [Test, Order(25)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithSmallCoverArt()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithSmallCoverArt();

            var bytes = await Cdn.GetReleaseCoverAsByteArray(builder);

            using (var stream = new MemoryStream(bytes))
            using (var image = Image.Load(stream))
            {
                Assert.GreaterOrEqual(image.Height, 300);
                Assert.GreaterOrEqual(image.Width, 300);
            }
        }

        [Test, Order(26)]
        public async Task Test_DownloadReleaseCoverAsStream()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithHugeCoverArt();

            using (var stream = await Cdn.GetReleaseCoverAsStream(builder))
            using (var image = Image.Load(stream))
            {
                Assert.Greater(image.Height, 0);
                Assert.Greater(image.Width, 0);
            }
        }

        [Test, Order(27)]
        public async Task Test_GetRelease_Returns_All_Fields()
        {
            var release = await Api.GetRelease("MCS1356");

            Assert.AreEqual("MCS1356", release.Release.CatalogId);

            Assert.NotNull(release.Release.Id);

            Assert.NotNull(release.Release.ArtistsTitle);
            Assert.NotNull(release.Release.Version);
            Assert.NotNull(release.Release.Title);
            Assert.NotNull(release.Release.Type);
            Assert.NotNull(release.Release.GenrePrimary);
            Assert.NotNull(release.Release.GenreSecondary);

            Assert.NotNull(release.Release.BrandId);
            Assert.NotNull(release.Release.BrandTitle);

            Assert.NotNull(release.Release.Links);
            Assert.IsNotEmpty(release.Release.Links);

            Assert.NotNull(release.Tracks);
            Assert.IsNotEmpty(release.Tracks);
        }

        [Test, Order(999)]
        public async Task Test_Logout()
        {
            await Api.Logout();
        }
    }
}
