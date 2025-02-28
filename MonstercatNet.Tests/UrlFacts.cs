namespace SoftThorn.MonstercatNet.Tests;

[Category(Categories.UnitTest)]
[Category(Categories.InMemory)]
public sealed class UrlFacts
{
    [Fact]
    public void ArtistPhotoBuilder_Should_Create_Valid_Request()
    {
        var builder = ArtistPhotoBuilder.Create(new Artist()
        {
            ArtistId = Guid.NewGuid(),
            Uri = "JohnDoe",
        });
        var request = builder.Build();

        Assert.NotNull(request);
        Assert.Multiple(() =>
        {
            Assert.NotEmpty(request.Url);
            Assert.NotEmpty(request.Encoding);
            Assert.True(request.Width > 0);

            Assert.True(Uri.TryCreate(request.Url, UriKind.RelativeOrAbsolute, out var uri));
        });
    }
}
