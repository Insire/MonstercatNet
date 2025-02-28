#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
namespace SoftThorn.MonstercatNet.Tests;

[Category(Categories.UnitTest)]
[Category(Categories.InMemory)]
public sealed class NullValidationTests(ApiTestFixture fixture) : IClassFixture<ApiTestFixture>
{
    private readonly ApiTestFixture _fixture = fixture;

    // there is no resource for this on the monstercat api - or atleast i didnt care to check
    private const string _randomInvalidGuid = "0788CAB5-4F38-4BEA-B7A0-F15D5A16888A";

    [Fact]
    public void Fact_CtorArgsForNull()
    {
        Assert.Throws<ArgumentNullException>(() => MonstercatApi.Create(null));
    }

    [Fact]
    public async Task Fact_LoginRequestForNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.Login(null));
    }

    [Fact]
    public async Task Fact_SearchTracksRequestForNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.SearchTracks(null));
    }

    [Fact]
    public async Task Fact_GetReleasesRequestForNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.GetReleases(null));
    }

    [Fact]
    public async Task Fact_GetReleaseRequestForNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.GetRelease(null));
    }

    [Fact]
    public async Task Fact_GetReleaseRequestForEmpty()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.GetRelease(string.Empty));
    }

    [Fact]
    public async Task Fact_DownloadTrackAsByteArrayRequestForNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.DownloadTrackAsByteArray(null));
    }

    [Fact]
    public async Task Fact_DownloadTrackAsByteArrayRequestForNullReleaseId()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _fixture.Api.DownloadTrackAsByteArray(new TrackDownloadRequest()
        {
            ReleaseId = Guid.Empty,
            TrackId = Guid.Parse(_randomInvalidGuid)
        }));
    }

    [Fact]
    public async Task Fact_DownloadTrackAsByteArrayRequestForNullTrackId()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _fixture.Api.DownloadTrackAsByteArray(new TrackDownloadRequest()
        {
            ReleaseId = Guid.Parse(_randomInvalidGuid),
            TrackId = Guid.Empty
        }));
    }

    [Fact]
    public async Task Fact_DownloadTrackAsStreamRequestForNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.DownloadTrackAsStream(null));
    }

    [Fact]
    public async Task Fact_StreamTrackAsStreamRequestForNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.StreamTrackAsStream(null));
    }

    [Fact]
    public async Task Fact_StreamTrackAsStreamRequestForNullReleaseId()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _fixture.Api.StreamTrackAsStream(new TrackStreamRequest()
        {
            ReleaseId = Guid.Empty,
            TrackId = Guid.Parse(_randomInvalidGuid)
        }));
    }

    [Fact]
    public async Task Fact_StreamTrackAsStreamRequestForNullTrackId()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _fixture.Api.StreamTrackAsStream(new TrackStreamRequest()
        {
            ReleaseId = Guid.Parse(_randomInvalidGuid),
            TrackId = Guid.Empty
        }));
    }

    [Fact]
    public async Task Fact_CreatePlaylistNullRequest()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.CreatePlaylist(null));
    }

    [Fact]
    public async Task Fact_CreatePlaylistNullName()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.CreatePlaylist(new PlaylistCreateRequest()
        {
            Title = null,
        }));
    }

    [Fact]
    public async Task Fact_DeletePlaylistEmptyPlaylistId()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.DeletePlaylist(Guid.Empty));
    }

    [Fact]
    public async Task Fact_PlaylistAddTrackNullRequest()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.PlaylistAddTrack(Guid.Parse(_randomInvalidGuid), null));
    }

    [Fact]
    public async Task Fact_PlaylistAddTrackEmptyPlaylistId()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.PlaylistAddTrack(Guid.Empty, null));
    }

    [Fact]
    public async Task Fact_PlaylistAddTrackNullPlaylistId()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.PlaylistAddTrack(Guid.Parse(_randomInvalidGuid), new PlaylistAddTrackRequest()
        {
            Records =
            [
                new PlaylistRecord()
                {
                    PlaylistId = Guid.Empty,
                    ReleaseId = Guid.Parse(_randomInvalidGuid),
                    TrackId = Guid.Parse(_randomInvalidGuid),
                }
            ]
        }));
    }

    [Fact]
    public async Task Fact_PlaylistAddTrackNullRelease()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.PlaylistAddTrack(Guid.Parse(_randomInvalidGuid), new PlaylistAddTrackRequest()
        {
            Records =
            [
                new PlaylistRecord()
                {
                    PlaylistId = Guid.Parse(_randomInvalidGuid),
                    ReleaseId = Guid.Empty,
                    TrackId = Guid.Parse(_randomInvalidGuid),
                }
            ]
        }));
    }

    [Fact]
    public async Task Fact_PlaylistAddTrackNullTrack()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.PlaylistAddTrack(Guid.Parse(_randomInvalidGuid), new PlaylistAddTrackRequest()
        {
            Records =
            [
                new PlaylistRecord()
                {
                    PlaylistId = Guid.Parse(_randomInvalidGuid),
                    ReleaseId = Guid.Parse(_randomInvalidGuid),
                    TrackId = Guid.Empty
                }
            ]
        }));
    }

    [Fact]
    public async Task Fact_PlaylistDeleteTrackNullPlaylistId()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.PlaylistDeleteTrack(Guid.Parse(_randomInvalidGuid), new PlaylistDeleteTrackRequest()
        {
            Records =
            [
                new PlaylistRecord()
                {
                    PlaylistId = Guid.Empty,
                    ReleaseId = Guid.Parse(_randomInvalidGuid),
                    TrackId = Guid.Parse(_randomInvalidGuid),
                }
            ]
        }));
    }

    [Fact]
    public async Task Fact_PlaylistDeleteTrackNullRelease()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.PlaylistDeleteTrack(Guid.Parse(_randomInvalidGuid), new PlaylistDeleteTrackRequest()
        {
            Records =
            [
                new PlaylistRecord()
                {
                    PlaylistId = Guid.Parse(_randomInvalidGuid),
                    ReleaseId = Guid.Empty,
                    TrackId = Guid.Parse(_randomInvalidGuid),
                }
            ]
        }));
    }

    [Fact]
    public async Task Fact_PlaylistDeleteTrackNullTrack()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.PlaylistDeleteTrack(Guid.Parse(_randomInvalidGuid), new PlaylistDeleteTrackRequest()
        {
            Records =
            [
                new PlaylistRecord()
                {
                    PlaylistId = Guid.Parse(_randomInvalidGuid),
                    ReleaseId = Guid.Parse(_randomInvalidGuid),
                    TrackId = Guid.Empty
                }
            ]
        }));
    }

    [Fact]
    public async Task Fact_PlaylistGetPlaylistNullPlaylistId()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.GetPlaylist(Guid.Empty, new GetPlaylistRequest()));
    }

    [Fact]
    public async Task Fact_PlaylistUpdatePlaylistNullPlaylistId()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.UpdatePlaylist(new UpdatePlaylistRequest()
        {
            Title = "1"
        }));
    }

    [Fact]
    public async Task Fact_PlaylistUpdatePlaylistNullrequest()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.UpdatePlaylist(null));
    }

    [Fact]
    public async Task Fact_PlaylistUpdatePlaylistNullPlaylistName()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.UpdatePlaylist(new UpdatePlaylistRequest()
        {
            PlaylistId = Guid.Empty,
            Title = null
        }));
    }
}
