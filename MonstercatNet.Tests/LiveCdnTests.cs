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
            var builder = ReleaseCoverArtBuilder
                            .Create()
                            .ForRelease(release);

            var cover = await Cdn.GetReleaseCoverAsByteArray(builder);

            Assert.That(cover, Is.Not.Null);
            Assert.That(cover, Is.Not.Empty);
        }

        [Test, Order(2)]
        public async Task Test_GetReleaseCoverAsStream()
        {
            var builder = ReleaseCoverArtBuilder
                            .Create()
                            .ForRelease(new TrackRelease()
                            {
                                CatalogId = "MCS1346",
                            });

            var cover = await Cdn.GetReleaseCoverAsStream(builder);

            Assert.That(cover, Is.Not.Null);

            var result = cover.ToByteArray();
            Assert.That(result, Is.Not.Empty);
        }

        [Test, Order(3)]
        public async Task Test_GetArtistPhotoAsStream()
        {
            var builder = ArtistPhotoBuilder
                            .Create(new Artist()
                            {
                                ArtistId = System.Guid.Parse("{bf6215c7-7dc6-45f8-873a-61973aee536b}"),
                                Uri = "lanidaye"
                            })
                            .WithLargePhoto();

            var cover = await Cdn.GetArtistPhotoAsStream(builder);

            Assert.That(cover, Is.Not.Null);

            var result = cover.ToByteArray();
            Assert.That(result, Is.Not.Empty);
        }
    }
}
