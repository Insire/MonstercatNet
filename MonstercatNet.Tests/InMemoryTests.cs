using NUnit.Framework;
using System;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class InMemoryTests
    {
        public sealed class LoginRequestValidation : TestBase
        {
            [Test]
            public void Test_NullEmail()
            {
                var request = new ApiCredentials()
                {
                    Email = null,
                    Password = "something"
                };

                Assert.Throws<ArgumentNullException>(() => Api.Login(request));
            }

            [Test]
            public void Test_EmptyEmail()
            {
                var request = new ApiCredentials()
                {
                    Email = "",
                    Password = "something"
                };

                Assert.Throws<ArgumentNullException>(() => Api.Login(request));
            }

            [Test]
            public void Test_NullPassword()
            {
                var request = new ApiCredentials()
                {
                    Email = "something",
                    Password = null
                };

                Assert.Throws<ArgumentNullException>(() => Api.Login(request));
            }

            [Test]
            public void Test_EmptyPassword()
            {
                var request = new ApiCredentials()
                {
                    Email = "something",
                    Password = null
                };

                Assert.Throws<ArgumentNullException>(() => Api.Login(request));
            }
        }

        public sealed class RequestBaseValidation
        {
            [Test]
            public void Test_LimitCantExceedMaxLimit()
            {
                var request = new TestRequest();
                Assert.AreEqual(RequestBase.MaxLimit, request.Limit);

                request.Limit = RequestBase.MaxLimit + 1;
                Assert.AreEqual(RequestBase.MaxLimit, request.Limit);
            }

            [Test]
            public void Test_LimitCantBeLowerThanMinLimit()
            {
                var request = new TestRequest();
                Assert.AreEqual(RequestBase.MaxLimit, request.Limit);

                request.Limit = RequestBase.MinLimit - 1;
                Assert.AreEqual(RequestBase.MinLimit, request.Limit);
            }

            [Test]
            public void Test_SkipCantBeLowerThanMinSkip()
            {
                var request = new TestRequest();
                Assert.AreEqual(RequestBase.MinSkip, request.Skip);

                request.Skip = RequestBase.MinSkip - 1;
                Assert.AreEqual(RequestBase.MinSkip, request.Skip);
            }

            private sealed class TestRequest : RequestBase
            {
            }
        }

        public sealed class NullValidation : TestBase
        {
            // there is no resource for this on the monstercat api - or atleast i didnt care to check
            private const string RandomInvalidGuid = "0788CAB5-4F38-4BEA-B7A0-F15D5A16888A";

            [Test]
            public void Test_CtorArgsForNull()
            {
                Assert.Throws<ArgumentNullException>(() => MonstercatApi.Create(null));
            }

            [Test]
            public void Test_LoginRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.Login(default(ApiCredentials)));
            }

            [Test]
            public void Test_Login2FAForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.Login(default(string)));
            }

            [Test]
            public void Test_Login2FAForEmpty()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.Login(string.Empty));
            }

            [Test]
            public void Test_Resend2FAForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.Resend(default(string)));
            }

            [Test]
            public void Test_Resend2FAForEmpty()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.Resend(string.Empty));
            }

            [Test]
            public void Test_SearchTracksRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.SearchTracks(null));
            }

            [Test]
            public void Test_GetReleasesRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.GetReleases(null));
            }

            [Test]
            public void Test_GetReleaseRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.GetRelease(null));
            }

            [Test]
            public void Test_GetReleaseRequestForEmpty()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.GetRelease(string.Empty));
            }

            [Test]
            public void Test_GetReleaseCoverAsByteArrayRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.GetReleaseCoverAsByteArray(null));
            }

            [Test]
            public void Test_GetReleaseCoverAsStreamRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.GetReleaseCoverAsStream(null));
            }

            [Test]
            public void Test_GetReleaseCoverAsByteArrayRequestForNullReleaseId()
            {
                Assert.ThrowsAsync<ArgumentException>(() => Api.GetReleaseCoverAsByteArray(new ReleaseCoverRequest()
                {
                    ReleaseId = Guid.Empty
                }));
            }

            [Test]
            public void Test_DownloadReleaseAsByteArrayRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.DownloadReleaseAsByteArray(null));
            }

            [Test]
            public void Test_DownloadReleaseAsByteArrayRequestForNullReleaseId()
            {
                Assert.ThrowsAsync<ArgumentException>(() => Api.DownloadReleaseAsByteArray(new ReleaseDownloadRequest()
                {
                    ReleaseId = Guid.Empty
                }));
            }

            [Test]
            public void Test_DownloadReleaseAsStreamRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.DownloadReleaseAsStream(null));
            }

            [Test]
            public void Test_DownloadTrackAsByteArrayRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.DownloadTrackAsByteArray(null));
            }

            [Test]
            public void Test_DownloadTrackAsByteArrayRequestForNullReleaseId()
            {
                Assert.ThrowsAsync<ArgumentException>(() => Api.DownloadTrackAsByteArray(new TrackDownloadRequest()
                {
                    ReleaseId = Guid.Empty,
                    TrackId = Guid.Parse(RandomInvalidGuid)
                }));
            }

            [Test]
            public void Test_DownloadTrackAsByteArrayRequestForNullTrackId()
            {
                Assert.ThrowsAsync<ArgumentException>(() => Api.DownloadTrackAsByteArray(new TrackDownloadRequest()
                {
                    ReleaseId = Guid.Parse(RandomInvalidGuid),
                    TrackId = Guid.Empty
                }));
            }

            [Test]
            public void Test_DownloadTrackAsStreamRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.DownloadTrackAsStream(null));
            }

            [Test]
            public void Test_StreamTrackAsStreamRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.StreamTrackAsStream(null));
            }

            [Test]
            public void Test_StreamTrackAsStreamRequestForNullReleaseId()
            {
                Assert.ThrowsAsync<ArgumentException>(() => Api.StreamTrackAsStream(new TrackStreamRequest()
                {
                    ReleaseId = Guid.Empty,
                    TrackId = Guid.Parse(RandomInvalidGuid)
                }));
            }

            [Test]
            public void Test_StreamTrackAsStreamRequestForNullTrackId()
            {
                Assert.ThrowsAsync<ArgumentException>(() => Api.StreamTrackAsStream(new TrackStreamRequest()
                {
                    ReleaseId = Guid.Parse(RandomInvalidGuid),
                    TrackId = Guid.Empty
                }));
            }

            [Test]
            public void Test_CreatePlaylistNullRequest()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.CreatePlaylist(null));
            }

            [Test]
            public void Test_CreatePlaylistNullName()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.CreatePlaylist(new PlaylistCreateRequest()
                {
                    Name = null,
                    Public = true,
                    Tracks = new PlaylistCreateTrack[0]
                }));
            }

            [Test]
            public void Test_DeletePlaylistEmptyPlaylistId()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.DeletePlaylist(Guid.Empty));
            }

            [Test]
            public void Test_PlaylistAddTrackNullRequest()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.PlaylistAddTrack(null));
            }

            [Test]
            public void Test_PlaylistAddTrackNullPlaylistId()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.PlaylistAddTrack(new PlaylistAddTrackRequest()
                {
                    PlaylistId = Guid.Empty,
                    ReleaseId = Guid.Parse(RandomInvalidGuid),
                    TrackId = Guid.Parse(RandomInvalidGuid),
                }));
            }

            [Test]
            public void Test_PlaylistAddTrackNullRelease()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.PlaylistAddTrack(new PlaylistAddTrackRequest()
                {
                    PlaylistId = Guid.Parse(RandomInvalidGuid),
                    ReleaseId = Guid.Empty,
                    TrackId = Guid.Parse(RandomInvalidGuid),
                }));
            }

            [Test]
            public void Test_PlaylistAddTrackNullTrack()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.PlaylistAddTrack(new PlaylistAddTrackRequest()
                {
                    PlaylistId = Guid.Parse(RandomInvalidGuid),
                    ReleaseId = Guid.Parse(RandomInvalidGuid),
                    TrackId = Guid.Empty
                }));
            }

            [Test]
            public void Test_PlaylistDeleteTrackNullRequest()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.PlaylistDeleteTrack(Guid.Parse(RandomInvalidGuid), null));
            }

            [Test]
            public void Test_PlaylistDeleteTrackNullPlaylistId()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.PlaylistDeleteTrack(Guid.Empty, new PlaylistDeleteTrackRequest()
                {
                    ReleaseId = Guid.Parse(RandomInvalidGuid),
                    TrackId = Guid.Parse(RandomInvalidGuid),
                }));
            }

            [Test]
            public void Test_PlaylistDeleteTrackNullRelease()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.PlaylistDeleteTrack(Guid.Parse(RandomInvalidGuid), new PlaylistDeleteTrackRequest()
                {
                    ReleaseId = Guid.Empty,
                    TrackId = Guid.Parse(RandomInvalidGuid),
                }));
            }

            [Test]
            public void Test_PlaylistDeleteTrackNullTrack()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.PlaylistDeleteTrack(Guid.Parse(RandomInvalidGuid), new PlaylistDeleteTrackRequest()
                {
                    ReleaseId = Guid.Parse(RandomInvalidGuid),
                    TrackId = Guid.Empty
                }));
            }

            [Test]
            public void Test_PlaylistGetTrackListNullPlaylistId()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.GetPlaylistTracks(Guid.Empty));
            }

            [Test]
            public void Test_PlaylistGetPlaylistNullPlaylistId()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.GetPlaylist(Guid.Empty));
            }
        }
    }
}
