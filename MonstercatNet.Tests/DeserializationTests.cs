using NUnit.Framework;
using SoftThorn.MonstercatNet.Tests.Resources;
using System.IO;
using System.Text.Json;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class DeserializationTests
    {
        [Test]
        public void Should_DeSerialize_GetPlaylists_Reposnse()
        {
            using var stream = typeof(DeserializationTests).Assembly.GetManifestResourceStream(typeof(MonstercatNetResources), "GetPlaylists.json")!;

            using var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            JsonSerializer.Deserialize<SelfPlaylistsResult>(text);
        }
    }
}
