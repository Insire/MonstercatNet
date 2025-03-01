#nullable disable

using SixLabors.ImageSharp;

namespace SoftThorn.MonstercatNet.Tests
{
    [Category(Categories.IntegrationTest)]
    public sealed class LiveApiTests(ApiTestFixture apiTextApiTextFixture, CdnTestFixture cdnTestFixture) : IClassFixture<ApiTestFixture>, IClassFixture<CdnTestFixture>
    {
        private readonly ApiTestFixture _apiTextFixture = apiTextApiTextFixture;
        private readonly CdnTestFixture _cdnTestFixture = cdnTestFixture;
        internal Guid ReleaseId { get; } = Guid.Parse("75c1a74c-27bc-4ef6-884b-0b56515ea6e0");
        internal Guid TrackId { get; } = Guid.Parse("f2db30c8-1547-4c41-93d9-dca2bc822cac");

        internal Guid? PlaylistId { get; private set; }
        internal Guid? UserId { get; private set; }

        [Fact, TestPriority(1)]
        public async Task Test_Login()
        {
            await _apiTextFixture.Api.Login(_apiTextFixture.Credentials);

            Assert.True(_apiTextFixture.IsLoggedIn);
        }

        [Fact, TestPriority(2)]
        public async Task Test_GetSelf()
        {
            Assert.True(_apiTextFixture.IsLoggedIn);

            var self = await _apiTextFixture.Api.GetSelf();

            Assert.Multiple(() =>
            {
                Assert.NotNull(self);
                Assert.NotNull(self.User);
                Assert.Equal(self.User.Email, _apiTextFixture.Credentials.Email);

                // The test account should have an active gold subscription, otherwise some tests are bound to fail.
                Assert.True(self.User.HasGold);
            });

            UserId = self.User.Id;
        }

        [Fact, TestPriority(3)]
        public async Task Test_GetTrackSearchFilters()
        {
            var filters = await _apiTextFixture.Api.GetTrackSearchFilters();

            Assert.NotNull(filters);
            Assert.Multiple(() =>
            {
                Assert.NotEmpty(filters.Genres);
                Assert.NotEmpty(filters.Brands);
                Assert.NotEmpty(filters.Types);
            });
        }

        [Fact, TestPriority(4)]
        public async Task Test_SearchTracks()
        {
            var tracks = await _apiTextFixture.Api.SearchTracks(new TrackSearchRequest()
            {
                Limit = 100,
                Skip = 0,
                Creatorfriendly = true,
                ReleaseTypes = ["EP"],
                Tags = ["silkinitialbulkimport"],
            });

            Assert.NotNull(tracks);
            Assert.NotEmpty(tracks.Results);
            var entry = tracks.Results.Single(p => p.Id == Guid.Parse("3d184df8-5ad5-4b69-8250-36363d16a6b8"));

            Assert.Multiple(() =>
            {
                Assert.NotNull(entry.Artists);
                Assert.NotNull(entry.ArtistsTitle);

                Assert.NotNull(tracks.Results[0].Artists[0]);
            });

            Assert.Multiple(() =>
            {
                Assert.NotEqual(entry.Artists[0].Id, Guid.Empty);
                Assert.NotEqual(entry.Artists[0].ProfileFileId, Guid.Empty);
                Assert.NotEqual(entry.Artists[0].CatalogRecordId, Guid.Empty);

                Assert.NotEmpty(entry.Artists[0].Name);
                Assert.NotEmpty(entry.Artists[0].Role);
                Assert.NotEmpty(entry.Artists[0].Uri);
            });
        }

        [Fact, TestPriority(5)]
        public async Task Test_SearchAllTracks()
        {
            var results = await _apiTextFixture.Api.SearchTracks(new TrackSearchRequest()
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
                results = await _apiTextFixture.Api.SearchTracks(new TrackSearchRequest()
                {
                    Limit = localLimit,
                    Skip = skip,
                });
                skip += localLimit;

                Validate(results);
            }

            static void Validate(TrackSearchResult results)
            {
                Assert.NotNull(results);
                Assert.NotEmpty(results.Results);

                foreach (var entry in results.Results)
                {
                    Assert.Multiple(() =>
                    {
                        Assert.NotNull(entry.Artists);
                        Assert.NotNull(entry.ArtistsTitle);

                        Assert.NotNull(results.Results[0].Artists[0]);
                    });

                    Assert.Multiple(() =>
                    {
                        Assert.NotEqual(entry.Artists[0].Id, Guid.Empty);
                        Assert.NotEqual(entry.Artists[0].ProfileFileId, Guid.Empty);
                        Assert.NotEqual(entry.Artists[0].CatalogRecordId, Guid.Empty);

                        Assert.NotEmpty(entry.Artists[0].Name);
                        Assert.NotEmpty(entry.Artists[0].Role);
                        Assert.NotEmpty(entry.Artists[0].Uri);
                    });
                }
            }
        }

        [Fact, TestPriority(6)]
        public async Task Test_GetReleases()
        {
            var releases = await _apiTextFixture.Api.GetReleases(new ReleaseBrowseRequest()
            {
                Limit = 1,
                Skip = 0
            });

            Assert.NotNull(releases);
            Assert.Multiple(() =>
            {
                Assert.NotNull(releases.Results);
                Assert.NotNull(releases.Results.Data);
                Assert.Single(releases.Results.Data);
                Assert.NotNull(releases.Results.Data[0]);
            });
        }

        [Fact, TestPriority(7)]
        public async Task Test_GetRelease()
        {
            var release = await _apiTextFixture.Api.GetRelease("MCRLX001-8");

            Assert.NotNull(release);
            Assert.Multiple(() =>
            {
                Assert.NotNull(release.Release);
                Assert.NotNull(release.Tracks);
            });
            Assert.Single(release.Tracks);
        }

        [Fact, TestPriority(8), Category(Categories.GoldMembershipRequired)]
        public async Task Test_DownloadTrackAsByteArray()
        {
            Assert.True(_apiTextFixture.IsLoggedIn);

            var release = await _apiTextFixture.Api.DownloadTrackAsByteArray(new TrackDownloadRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.NotNull(release);
            Assert.NotEmpty(release);
        }

        [Fact, TestPriority(9), Category(Categories.GoldMembershipRequired)]
        public async Task Test_DownloadTrackAsStream()
        {
            Assert.True(_apiTextFixture.IsLoggedIn);

            var release = await _apiTextFixture.Api.DownloadTrackAsStream(new TrackDownloadRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.NotNull(release);

            var result = release.ToByteArray();
            Assert.NotEmpty(result);
        }

        [Fact, TestPriority(10)]
        public async Task Test_StreamTrackAsStream()
        {
            var release = await _apiTextFixture.Api.StreamTrackAsStream(new TrackStreamRequest()
            {
                ReleaseId = Guid.Parse("09497970-9679-4ea6-930d-e1bf22cfc994"),
                TrackId = Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f")
            });

            Assert.NotNull(release);

            var result = release.ToByteArray();
            Assert.NotEmpty(result);
        }

        [Fact, TestPriority(11)]
        public async Task Test_CreatePlaylist()
        {
            Assert.True(_apiTextFixture.IsLoggedIn);

            var response = await _apiTextFixture.Api.CreatePlaylist(new PlaylistCreateRequest()
            {
                Title = "MyTestPlaylist",
            });

            Assert.NotNull(response);

            PlaylistId = response.Id;
        }

        [Fact, TestPriority(12)]
        public async Task Test_PlaylistAddTrack()
        {
            Assert.NotNull(PlaylistId);

            await _apiTextFixture.Api.PlaylistAddTrack(PlaylistId.Value, new PlaylistAddTrackRequest()
            {
                Records =
                [
                    new PlaylistRecord()
                    {
                        PlaylistId = PlaylistId.Value,
                        ReleaseId = ReleaseId,
                        TrackId = TrackId,
                    }
                ]
            });
        }

        [Fact, TestPriority(13)]
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
            var result = await _apiTextFixture.Api.GetPlaylist(playlistId, request);

            Assert.NotNull(result);
            Assert.Multiple(() =>
            {
                Assert.True(result.Total > 500);

                Assert.NotNull(result.Tracks);
            });
            Assert.NotEmpty(result.Tracks);

            Assert.True(result.Total >= result.Tracks.Length);

            var total = result.Total;
            var localLimit = result.Limit;
            var skip = result.Offset + localLimit;
            while (skip < total)
            {
                request.Limit = localLimit;
                request.Skip = skip;
                result = await _apiTextFixture.Api.GetPlaylist(playlistId, request);
                skip += localLimit;

                Validate(result);
            }

            static void Validate(GetPlaylistResult results)
            {
                Assert.NotNull(results);
                Assert.NotEmpty(results.Tracks);

                foreach (var entry in results.Tracks)
                {
                    Assert.Multiple(() =>
                    {
                        Assert.NotNull(entry.Artists);
                        Assert.NotNull(entry.ArtistsTitle);

                        Assert.NotNull(results.Tracks[0].Artists[0]);
                    });

                    Assert.Multiple(() =>
                    {
                        Assert.NotEqual(entry.Artists[0].Id, Guid.Empty);
                        Assert.NotEqual(entry.Artists[0].ProfileFileId, Guid.Empty);
                        Assert.NotEqual(entry.Artists[0].CatalogRecordId, Guid.Empty);

                        Assert.NotEmpty(entry.Artists[0].Name);
                        Assert.NotEmpty(entry.Artists[0].Role);
                        Assert.NotEmpty(entry.Artists[0].Uri);
                    });
                }
            }
        }

        [Fact, TestPriority(14)]
        public async Task Test_GetSelfPlaylists()
        {
            Assert.True(_apiTextFixture.IsLoggedIn);

            var playlists = await _apiTextFixture.Api.GetSelfPlaylists();

            Assert.NotNull(playlists);

            Assert.Multiple(() =>
            {
                Assert.NotNull(playlists.Playlists);
                Assert.NotNull(playlists.Playlists.Data);
                Assert.NotEmpty(playlists.Playlists.Data);
                Assert.Contains(playlists.Playlists.Data, p => p.Id == PlaylistId);
            });
        }

        [Fact, TestPriority(15)]
        public async Task Test_PlaylistDeleteTrack()
        {
            Assert.NotNull(PlaylistId);

            await _apiTextFixture.Api.PlaylistDeleteTrack(PlaylistId.Value, new PlaylistDeleteTrackRequest()
            {
                Records =
                [
                    new PlaylistRecord()
                    {
                        PlaylistId = PlaylistId.Value,
                        ReleaseId = ReleaseId,
                        TrackId = TrackId,
                    }
                ]
            });
        }

        [Fact, TestPriority(16)]
        public async Task Test_UpdatePlaylist()
        {
            Assert.NotNull(PlaylistId);

            var playlist = await _apiTextFixture.Api.UpdatePlaylist(new UpdatePlaylistRequest()
            {
                Title = "MyRenameTestPlaylist",
                PlaylistId = PlaylistId.Value,
                UserId = UserId.Value,
            });

            Assert.NotNull(playlist);
            Assert.Equal("MyRenameTestPlaylist", playlist.Title);
        }

        [Fact, TestPriority(17)]
        public async Task Test_MakePlaylistPublic()
        {
            Assert.NotNull(PlaylistId);

            var playlist = await _apiTextFixture.Api.UpdatePlaylist(new UpdatePlaylistRequest()
            {
                Title = null,
                PlaylistId = PlaylistId.Value,
                UserId = UserId.Value,
                IsPublic = true,
            });

            Assert.True(playlist.IsPublic);
        }

        [Fact, TestPriority(18)]
        public async Task Test_MakePlaylistPrivate()
        {
            Assert.NotNull(PlaylistId);

            var playlist = await _apiTextFixture.Api.UpdatePlaylist(new UpdatePlaylistRequest()
            {
                Title = null,
                PlaylistId = PlaylistId.Value,
                UserId = UserId.Value,
                IsPublic = false,
            });

            Assert.False(playlist.IsPublic);
        }

        [Fact, TestPriority(19)]
        public async Task Test_DeletePlaylist()
        {
            Assert.NotNull(PlaylistId);

            await _apiTextFixture.Api.DeletePlaylist(PlaylistId.Value);
        }

        [Fact, TestPriority(20)]
        public async Task Test_DownloadArtistPhoto_WithHugePhoto()
        {
            var builder = ArtistPhotoBuilder.Create(new Artist()
            {
                ArtistId = Guid.Parse("{4f2c83b1-7a08-42df-bf1c-d1341b8982ae}"),
                Uri = "aftruu",
            }).WithHugePhoto();

            await using var stream = await _cdnTestFixture.Cdn.GetArtistPhotoAsStream(builder);
            using var image = await Image.LoadAsync(stream);

            Assert.True(image.Height >= 3000);
            Assert.True(image.Width >= 3000);
        }

        [Fact, TestPriority(21)]
        public async Task Test_DownloadArtistPhoto_WithLargePhoto()
        {
            var builder = ArtistPhotoBuilder.Create(new Artist()
            {
                ArtistId = Guid.Parse("{4f2c83b1-7a08-42df-bf1c-d1341b8982ae}"),
                Uri = "aftruu",
            }).WithLargePhoto();

            await using var stream = await _cdnTestFixture.Cdn.GetArtistPhotoAsStream(builder);
            using var image = await Image.LoadAsync(stream);

            Assert.True(image.Height >= 1024);
            Assert.True(image.Width >= 1024);
        }

        [Fact, TestPriority(22)]
        public async Task Test_DownloadArtistPhoto_WithSmallPhoto()
        {
            var builder = ArtistPhotoBuilder.Create(new Artist()
            {
                ArtistId = Guid.Parse("{4f2c83b1-7a08-42df-bf1c-d1341b8982ae}"),
                Uri = "aftruu",
            }).WithSmallPhoto();

            await using var stream = await _cdnTestFixture.Cdn.GetArtistPhotoAsStream(builder);
            using var image = await Image.LoadAsync(stream);

            Assert.True(image.Height >= 256);
            Assert.True(image.Width >= 256);
        }

        [Fact, TestPriority(23)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithHugeCoverArt()
        {
            var release = await _apiTextFixture.Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithHugeCoverArt();

            var bytes = await _cdnTestFixture.Cdn.GetReleaseCoverAsByteArray(builder);

            await using var stream = new MemoryStream(bytes);
            using var image = await Image.LoadAsync(stream);

            Assert.True(image.Height >= 3000);
            Assert.True(image.Width >= 3000);
        }

        [Fact, TestPriority(24)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithLargeCoverArt()
        {
            var release = await _apiTextFixture.Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithLargeCoverArt();

            var bytes = await _cdnTestFixture.Cdn.GetReleaseCoverAsByteArray(builder);

            await using var stream = new MemoryStream(bytes);
            using var image = await Image.LoadAsync(stream);

            Assert.True(image.Height >= 1024);
            Assert.True(image.Width >= 1024);
        }

        [Fact, TestPriority(25)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithMediumCoverArt()
        {
            var release = await _apiTextFixture.Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithMediumCoverArt();

            var bytes = await _cdnTestFixture.Cdn.GetReleaseCoverAsByteArray(builder);

            await using var stream = new MemoryStream(bytes);
            using var image = await Image.LoadAsync(stream);

            Assert.True(image.Height >= 600);
            Assert.True(image.Width >= 600);
        }

        [Fact, TestPriority(26)]
        public async Task Test_DownloadReleaseCoverAsBytes_WithSmallCoverArt()
        {
            var release = await _apiTextFixture.Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithSmallCoverArt();

            var bytes = await _cdnTestFixture.Cdn.GetReleaseCoverAsByteArray(builder);

            await using var stream = new MemoryStream(bytes);
            using var image = await Image.LoadAsync(stream);

            Assert.True(image.Height >= 300);
            Assert.True(image.Width >= 300);
        }

        [Fact, TestPriority(27)]
        public async Task Test_DownloadReleaseCoverAsStream()
        {
            var release = await _apiTextFixture.Api.GetRelease("MCRLX001-8");
            var track = release.Tracks[0];

            var builder = ReleaseCoverArtBuilder.Create(track).WithHugeCoverArt();

            await using var stream = await _cdnTestFixture.Cdn.GetReleaseCoverAsStream(builder);
            using var image = await Image.LoadAsync(stream);

            Assert.True(image.Height > 0);
            Assert.True(image.Width > 0);
        }

        [Fact, TestPriority(28)]
        public async Task Test_GetRelease_Returns_All_Fields()
        {
            var release = await _apiTextFixture.Api.GetRelease("MCS1356");

            Assert.Multiple(() =>
            {
                Assert.Equal("MCS1356", release.Release?.CatalogId);

                Assert.NotNull(release.Release?.Id);

                Assert.NotNull(release.Release?.ArtistsTitle);
                Assert.NotNull(release.Release?.Version);
                Assert.NotNull(release.Release?.Title);
                Assert.NotNull(release.Release?.Type);
                Assert.NotNull(release.Release?.GenrePrimary);
                Assert.NotNull(release.Release?.GenreSecondary);

                Assert.NotNull(release.Release?.BrandId);
                Assert.NotNull(release.Release?.BrandTitle);

                Assert.NotNull(release.Release);
                Assert.NotNull(release.Release.Links);
                Assert.NotEmpty(release.Release.Links);

                Assert.NotNull(release.Tracks);
                Assert.NotEmpty(release.Tracks);
            });
        }

        [Fact, TestPriority(999)]
        public async Task Test_Logout()
        {
            Assert.True(_apiTextFixture.IsLoggedIn);

            await _apiTextFixture.Api.Logout();
        }
    }
}
