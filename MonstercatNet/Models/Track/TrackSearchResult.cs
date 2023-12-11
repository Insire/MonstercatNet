using System;
using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    /// <summary>
    /// represents tracks in the catalog, in reverse chronological order (newest first)
    /// </summary>
    public sealed class TrackSearchResult : ResultBase
    {
        [JsonPropertyName("Data")]
        public Track[] Results { get; set; } = Array.Empty<Track>();

        public object? NotFound { get; set; }
    }
}
