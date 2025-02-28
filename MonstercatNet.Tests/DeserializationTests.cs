using SoftThorn.MonstercatNet.Tests.Resources;

namespace SoftThorn.MonstercatNet.Tests
{
    [Category(Categories.UnitTest)]
    public sealed class DeserializationTests
    {
        [Fact]
        public void Should_DeSerialize_GetPlaylists_Response()
        {
            using var stream = typeof(DeserializationTests).Assembly.GetManifestResourceStream(typeof(MonstercatNetResources), "GetPlaylists.json")!;

            using var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            JsonSerializer.Deserialize<SelfPlaylistsResult>(text);
        }
    }
}
