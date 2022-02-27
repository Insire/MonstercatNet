using Newtonsoft.Json;

namespace SoftThorn.MonstercatNet
{
    public sealed class Fields
    {
        /// <summary>
        /// System.Collections.Generic.KeyValuePair<int, bool>[]
        /// </summary>
        [JsonProperty("archived")]
        public string[]? Archived { get; set; }

        /// <summary>
        /// System.Collections.Generic.KeyValuePair<int, bool>[]
        /// </summary>
        [JsonProperty("mylibrary")]
        public string[]? Mylibrary { get; set; }
    }
}
