using Newtonsoft.Json;

namespace SoftThorn.MonstercatNet
{
    public sealed class Fields
    {
        [JsonProperty("archived")]
        public string[]? Archived { get; set; }
    }
}
