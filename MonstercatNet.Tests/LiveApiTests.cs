#nullable disable

using FluentAssertions;
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

            Assert.That(IsLoggedIn, Is.True);
        }

        [Test, Order(2)]
        public async Task Test_GetSelf()
        {
            var self = await Api.GetSelf();

            Assert.That(self, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(self.User.Email, Is.EqualTo(Credentials.Email));
                Assert.That(self.User.HasGold, Is.True, "The test account should have an active gold subscription, otherwise some tests are bound to fail.");
            });

            UserId = self.User.Id;
        }

        [Test, Order(3)]
        public async Task Test_GetTrackSearchFilters()
        {
            var filters = await Api.GetTrackSearchFilters();

            Assert.That(filters, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(filters.Genres, Is.Not.Empty);
                Assert.That(filters.Tags, Is.Not.Empty);
                Assert.That(filters.Types, Is.Not.Empty);
            });
        }

        [Test, Order(4)]
        public async Task Test_SearchTracks()
        {
            var tracks = await Api.SearchTracks(new TrackSearchRequest()
            {
                Limit = 100,
                Skip = 0,
                Creatorfriendly = true,
                ReleaseTypes = new[] { "EP" },
                Tags = new[] { "silkinitialbulkimport" },
            });

            Assert.That(tracks, Is.Not.Null);
            Assert.That(tracks.Results, Is.Not.Empty);
            var entry = tracks.Results.Single(p => p.Id == Guid.Parse("65c9d857-4f34-4ad7-925c-fefb92e4d36d"));

            Assert.Multiple(() =>
            {
                Assert.That(entry.Artists, Is.Not.Null);
                Assert.That(entry.ArtistsTitle, Is.Not.Null);

                Assert.That(tracks.Results[0].Artists[0], Is.Not.Null);
            });

            Assert.Multiple(() =>
            {
                Assert.That(entry.Artists[0].Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(entry.Artists[0].ProfileFileId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(entry.Artists[0].CatalogRecordId, Is.Not.EqualTo(Guid.Empty));

                Assert.That(entry.Artists[0].Name, Is.Not.EqualTo(string.Empty));
                Assert.That(entry.Artists[0].Role, Is.Not.EqualTo(string.Empty));
                Assert.That(entry.Artists[0].Uri, Is.Not.EqualTo(string.Empty));
            });
        }

        [Test, Order(5)]
        public async Task Test_SearchAllTracks()
        {
            var results = await Api.SearchTracks(new TrackSearchRequest()
            {
                Limit = 100,
                Skip = 0,
            });

            Validate(results);

            var total = results.Total;
            var localLimit = results.Limit;
            var skip = results.Offset + localLimit;

            while (skip < total)
            {
                results = await Api.SearchTracks(new TrackSearchRequest()
                {
                    Limit = localLimit,
                    Skip = skip,
                });
                skip += localLimit;

                Validate(results);
            }

            static void Validate(TrackSearchResult results)
            {
                Assert.That(results, Is.Not.Null);
                Assert.That(results.Results, Is.Not.Empty);

                foreach (var entry in results.Results)
                {
                    Assert.Multiple(() =>
                    {
                        Assert.That(entry.Artists, Is.Not.Null);
                        Assert.That(entry.ArtistsTitle, Is.Not.Null);

                        Assert.That(results.Results[0].Artists[0], Is.Not.Null);
                    });

                    Assert.Multiple(() =>
                    {
                        Assert.That(entry.Artists[0].Id, Is.Not.EqualTo(Guid.Empty));
                        Assert.That(entry.Artists[0].ProfileFileId, Is.Not.EqualTo(Guid.Empty));
                        Assert.That(entry.Artists[0].CatalogRecordId, Is.Not.EqualTo(Guid.Empty));

                        Assert.That(entry.Artists[0].Name, Is.Not.EqualTo(string.Empty));
                        Assert.That(entry.Artists[0].Role, Is.Not.EqualTo(string.Empty));
                        Assert.That(entry.Artists[0].Uri, Is.Not.EqualTo(string.Empty));
                    });
                }
            }
        }

        [Test, Order(6)]
        public async Task Test_GetReleases()
        {
            var releases = await Api.GetReleases(new ReleaseBrowseRequest()
            {
                Limit = 1,
                Skip = 0
            });

            Assert.That(releases, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(releases.Results.Data, Has.Length.EqualTo(1));
                Assert.That(releases.Results.Data[0], Is.Not.Null);
            });
        }

        [Test, Order(7)]
        public async Task Test_GetRelease()
        {
            var release = await Api.GetRelease("MCRLX001-8");

            Assert.That(release, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(release.Release, Is.Not.Null);
                Assert.That(release.Tracks, Is.Not.Null);
            });
            Assert.That(release.Tracks, Has.Length.EqualTo(1));
        }

        // requires active gold subscription
        [Test, Order(8)]
        public async Task Test_DownloadTrackAsByteArray()
        {
            var release = await Api.DownloadTrackAsByteArray(new TrackDownloadRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.That(release, Is.Not.Null);
            Assert.That(release, Is.Not.Empty);
        }

        // requires active gold subscription
        [Test, Order(9)]
        public async Task Test_DownloadTrackAsStream()
        {
            var release = await Api.DownloadTrackAsStream(new TrackDownloadRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.That(release, Is.Not.Null);

            var result = release.ToByteArray();
            Assert.That(result, Is.Not.Empty);
        }

        [Test, Order(10)]
        public async Task Test_StreamTrackAsStream()
        {
            var release = await Api.StreamTrackAsStream(new TrackStreamRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.That(release, Is.Not.Null);

            var result = release.ToByteArray();
            Assert.That(result, Is.Not.Empty);
        }

        [Test, Order(11)]
        public async Task Test_CreatePlaylist()
        {
            var response = await Api.CreatePlaylist(new PlaylistCreateRequest()
            {
                Title = "MyTestPlaylist",
            });

            Assert.That(response, Is.Not.Null);

            PlaylistId = response.Id;
        }

        [Test, Order(12)]
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

        [Test, Order(13)]
        public async Task Test_GetPlaylist()
        {
            // call of the wild playlist with 500++ entries
            var playlistId = Guid.Parse("{5a68f0b7-4d98-4f9b-ae83-b9228e5af980}");

            var request = new GetPlaylistRequest()
            {
                Creatorfriendly = false,
                Limit = 100,
                NoGold = false,
                Skip = 0,
                StreamerMode = false,
            };
            var result = await Api.GetPlaylist(playlistId, request);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Total, Is.GreaterThan(500));

                Assert.That(result.Tracks, Is.Not.Null);
            });
            Assert.That(result.Tracks, Is.Not.Empty);

            result.Total.Should().BeGreaterThanOrEqualTo(result.Tracks.Length);

            var total = result.Total;
            var localLimit = result.Limit;
            var skip = result.Offset + localLimit;
            while (skip < total)
            {
                request.Limit = localLimit;
                request.Skip = skip;
                result = await Api.GetPlaylist(playlistId, request);
                skip += localLimit;

                Validate(result);
            }

            static void Validate(GetPlaylistResult results)
            {
                Assert.That(results, Is.Not.Null);
                Assert.That(results.Tracks, Is.Not.Empty);

                foreach (var entry in results.Tracks)
                {
                    Assert.Multiple(() =>
                    {
                        Assert.That(entry.Artists, Is.Not.Null);
                        Assert.That(entry.ArtistsTitle, Is.Not.Null);

                        Assert.That(results.Tracks[0].Artists[0], Is.Not.Null);
                    });

                    Assert.Multiple(() =>
                    {
                        Assert.That(entry.Artists[0].Id, Is.Not.EqualTo(Guid.Empty));
                        Assert.That(entry.Artists[0].ProfileFileId, Is.Not.EqualTo(Guid.Empty));
                        Assert.That(entry.Artists[0].CatalogRecordId, Is.Not.EqualTo(Guid.Empty));

                        Assert.That(entry.Artists[0].Name, Is.Not.EqualTo(string.Empty));
                        Assert.That(entry.Artists[0].Role, Is.Not.EqualTo(string.Empty));
                        Assert.That(entry.Artists[0].Uri, Is.Not.EqualTo(string.Empty));
                    });
                }
            }
        }

        [Test, Order(14)]
        public async Task Test_GetSelfPlaylists()
        {
            var playlists = await Api.GetSelfPlaylists();

            Assert.That(playlists, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(playlists.Playlists.Data, Is.Not.Empty);
                Assert.That(playlists.Playlists.Data.Any(p => p.Id == PlaylistId), Is.True);
            });
        }

        [Test, Order(15)]
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

        [Test, Order(16)]
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

            Assert.That(playlist.Title, Is.EqualTo("MyRenameTestPlaylist"));
        }

        [Test, Order(17)]
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

            Assert.That(playlist.IsPublic, Is.EqualTo(true));
        }

        [Test, Order(18)]
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

            Assert.That(playlist.IsPublic, Is.EqualTo(false));
        }

        [Test, Order(19)]
        public async Task Test_DeletePlaylist()
        {
            if (PlaylistId is null)
            {
                Assert.Inconclusive("The test case that should create a valid playlist either didn't run or did failed to complete.");
            }

            await Api.DeletePlaylist(PlaylistId.Value);
        }

        [Test, Order(20)]
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
                    Assert.Multiple(() =>
                    {
                        Assert.That(image.Height, Is.GreaterThanOrEqualTo(3000));
                        Assert.That(image.Width, Is.GreaterThanOrEqualTo(3000));
                    });
                }
            }
        }

        [Test, Order(21)]
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
                    Assert.Multiple(() =>
                    {
                        Assert.That(image.Height, Is.GreaterThanOrEqualTo(1024));
                        Assert.That(image.Width, Is.GreaterThanOrEqualTo(1024));
                    });
                }
            }
        }

        [Test, Order(22)]
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
                    Assert.Multiple(() =>
                    {
                        Assert.That(image.Height, Is.GreaterThanOrEqualTo(256));
                        Assert.That(image.Width, Is.GreaterThanOrEqualTo(256));
                    });
                }
            }
        }

        [Test, Order(23)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithHugeCoverArt()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithHugeCoverArt();

            var bytes = await Cdn.GetReleaseCoverAsByteArray(builder);

            using (var stream = new MemoryStream(bytes))
            using (var image = Image.Load(stream))
            {
                Assert.Multiple(() =>
                {
                    Assert.That(image.Height, Is.GreaterThanOrEqualTo(3000));
                    Assert.That(image.Width, Is.GreaterThanOrEqualTo(3000));
                });
            }
        }

        [Test, Order(24)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithLargeCoverArt()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithLargeCoverArt();

            var bytes = await Cdn.GetReleaseCoverAsByteArray(builder);

            using (var stream = new MemoryStream(bytes))
            using (var image = Image.Load(stream))
            {
                Assert.Multiple(() =>
                {
                    Assert.That(image.Height, Is.GreaterThanOrEqualTo(1024));
                    Assert.That(image.Width, Is.GreaterThanOrEqualTo(1024));
                });
            }
        }

        [Test, Order(25)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithMediumCoverArt()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithMediumCoverArt();

            var bytes = await Cdn.GetReleaseCoverAsByteArray(builder);

            using (var stream = new MemoryStream(bytes))
            using (var image = Image.Load(stream))
            {
                Assert.Multiple(() =>
                {
                    Assert.That(image.Height, Is.GreaterThanOrEqualTo(600));
                    Assert.That(image.Width, Is.GreaterThanOrEqualTo(600));
                });
            }
        }

        [Test, Order(26)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithSmallCoverArt()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithSmallCoverArt();

            var bytes = await Cdn.GetReleaseCoverAsByteArray(builder);

            using (var stream = new MemoryStream(bytes))
            using (var image = Image.Load(stream))
            {
                Assert.Multiple(() =>
                {
                    Assert.That(image.Height, Is.GreaterThanOrEqualTo(300));
                    Assert.That(image.Width, Is.GreaterThanOrEqualTo(300));
                });
            }
        }

        [Test, Order(27)]
        public async Task Test_DownloadReleaseCoverAsStream()
        {
            var release = await Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithHugeCoverArt();

            using (var stream = await Cdn.GetReleaseCoverAsStream(builder))
            using (var image = Image.Load(stream))
            {
                Assert.Multiple(() =>
                {
                    Assert.That(image.Height, Is.GreaterThan(0));
                    Assert.That(image.Width, Is.GreaterThan(0));
                });
            }
        }

        [Test, Order(28)]
        public async Task Test_GetRelease_Returns_All_Fields()
        {
            var release = await Api.GetRelease("MCS1356");

            Assert.Multiple(() =>
            {
                Assert.That(release.Release?.CatalogId, Is.EqualTo("MCS1356"));

                Assert.That(release.Release?.Id, Is.Not.Null);

                Assert.That(release.Release?.ArtistsTitle, Is.Not.Null);
                Assert.That(release.Release?.Version, Is.Not.Null);
                Assert.That(release.Release?.Title, Is.Not.Null);
                Assert.That(release.Release?.Type, Is.Not.Null);
                Assert.That(release.Release?.GenrePrimary, Is.Not.Null);
                Assert.That(release.Release?.GenreSecondary, Is.Not.Null);

                Assert.That(release.Release?.BrandId, Is.Not.Null);
                Assert.That(release.Release?.BrandTitle, Is.Not.Null);

                Assert.That(release.Release?.Links, Is.Not.Null);
                Assert.That(release.Release?.Links, Is.Not.Empty);

                Assert.That(release.Tracks, Is.Not.Null);
                Assert.That(release.Tracks, Is.Not.Empty);
            });
        }

        [Test, Order(999)]
        public async Task Test_Logout()
        {
            await Api.Logout();
        }
    }
}
