using NUnit.Framework;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class LiveCdnTests : CdnTestBase
    {
        [Test, Order(1)]
        public async Task Test_GetReleaseCoverAsByteArray()
        {
            var release = new TrackRelease()
            {
                CatalogId = "2FMCS1347",
            };
            var builder = ReleaseCoverArtBuilder.Create()
                .ForRelease(release);

            var cover = await Cdn.GetReleaseCoverAsByteArray(builder);

            Assert.IsNotNull(cover);
            Assert.IsTrue(cover.Length > 0);
        }

        [Test, Order(2)]
        public async Task Test_GetReleaseCoverAsStream()
        {
            var builder = ReleaseCoverArtBuilder.Create()
                .ForRelease(new TrackRelease()
                {
                    CatalogId = "MCS1346",
                });
            var cover = await Cdn.GetReleaseCoverAsStream(builder);

            Assert.IsNotNull(cover);

            var result = cover.ToByteArray();
            Assert.IsTrue(result.Length > 0);
        }
    }
}
