using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class Fields
    {
        /// <summary>
        /// System.Collections.Generic.KeyValuePair<int, bool>[]
        /// </summary>
        [JsonPropertyName("archived")]
        public string[]? Archived { get; set; }

        /// <summary>
        /// System.Collections.Generic.KeyValuePair<int, bool>[]
        /// </summary>
        [JsonPropertyName("mylibrary")]
        public string[]? Mylibrary { get; set; }
    }
}
