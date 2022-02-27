using Newtonsoft.Json;

namespace SoftThorn.MonstercatNet
{
    public sealed class ReleaseBrowseResult
    {
        [JsonProperty("Releases")]
        public Releases? Results { get; set; }
    }
}
