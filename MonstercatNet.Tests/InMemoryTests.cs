using NUnit.Framework;
using System;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class InMemoryTests
    {
        public sealed class RequestValidation
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
            public void Test_DownloadReleaseAsByteArrayRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.DownloadReleaseAsByteArray(null));
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
            public void Test_DownloadTrackAsStreamRequestForNull()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => Api.DownloadTrackAsStream(null));
            }
        }
    }
}
