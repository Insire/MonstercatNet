using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class ReleaseBrowseResult
    {
        [JsonPropertyName("Releases")]
        public Releases? Results { get; set; }
    }
}
