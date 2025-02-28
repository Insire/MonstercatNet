namespace SoftThorn.MonstercatNet.Tests
{
    [Category(Categories.IntegrationTest)]
    public sealed class LiveCdnTests(CdnTestFixture fixture) : IClassFixture<CdnTestFixture>
    {
        private readonly CdnTestFixture _fixture = fixture;

        [Fact, TestPriority(1)]
        public async Task Test_GetReleaseCoverAsByteArray()
        {
            var release = new TrackRelease()
            {
                CatalogId = "2FMCS1347",
            };
            var builder = ReleaseCoverArtBuilder
                            .Create()
                            .ForRelease(release);

            var cover = await _fixture.Cdn.GetReleaseCoverAsByteArray(builder);

            Assert.NotNull(cover);
            Assert.NotEmpty(cover);
        }

        [Fact, TestPriority(2)]
        public async Task Test_GetReleaseCoverAsStream()
        {
            var builder = ReleaseCoverArtBuilder
                            .Create()
                            .ForRelease(new TrackRelease()
                            {
                                CatalogId = "MCS1346",
                            });

            var cover = await _fixture.Cdn.GetReleaseCoverAsStream(builder);

            Assert.NotNull(cover);

            var result = cover.ToByteArray();
            Assert.NotEmpty(result);
        }

        [Fact, TestPriority(3)]
        public async Task Test_GetArtistPhotoAsStream()
        {
            var builder = ArtistPhotoBuilder
                            .Create(new Artist()
                            {
                                ArtistId = Guid.Parse("{bf6215c7-7dc6-45f8-873a-61973aee536b}"),
                                Uri = "lanidaye"
                            })
                            .WithLargePhoto();

            var cover = await _fixture.Cdn.GetArtistPhotoAsStream(builder);

            Assert.NotNull(cover);

            var result = cover.ToByteArray();
            Assert.NotEmpty(result);
        }
    }
}
