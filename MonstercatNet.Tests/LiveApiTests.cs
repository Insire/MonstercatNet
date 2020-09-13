using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class LiveApiTests : TestBase
    {
        [Test, Order(1)]
        public async Task Test_Login()
        {
            await Api.Login(Credentials);
        }

        [Test, Order(2)]
        public async Task Test_GetSelf()
        {
            var self = await Api.GetSelf();

            Assert.IsNotNull(self);
            Assert.AreEqual(Credentials.Email, self.Email);
            Assert.IsTrue(self.HasGold, "The test account should have an active gold subscription, otherwise some tests are bound to fail.");
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
                Genres = new[] { "Drumstep" },
                ReleaseTypes = new[] { "Album" },
                Tags = new[] { "Uncaged", "Energetic" },
            });

            Assert.IsNotNull(tracks);
            Assert.AreEqual(1, tracks.Results.Length);
            Assert.AreEqual(Guid.Parse("c8d3abc3-1668-42de-b832-b58ca6cc883f"), tracks.Results[0].Id);
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
            Assert.IsTrue(releases.Results.Length == 1);
            Assert.IsNotNull(releases.Results[0]);
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

        [Test, Order(7)]
        public async Task Test_GetReleaseCover()
        {
            var cover = await Api.GetReleaseCoverAsByteArray(new ReleaseCoverRequest()
            {
                ReleaseId = Guid.Parse("466c62cd-cfa8-457d-9dbf-66db101d73a6"),
            });

            Assert.IsNotNull(cover);
            Assert.IsTrue(cover.Length > 0);
        }

        [Test, Order(999)]
        public async Task Test_Logout()
        {
            await Api.Logout();
        }
    }
}
