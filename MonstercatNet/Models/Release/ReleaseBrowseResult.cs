using Newtonsoft.Json;

namespace SoftThorn.MonstercatNet
{
    public sealed class ReleaseBrowseResult : ResultBase
    {
        [JsonProperty("Releases")]
        public Releases? Results { get; set; }
    }
}
