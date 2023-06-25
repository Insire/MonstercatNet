using System;
using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistResult : ResultBase
    {
        [JsonPropertyName("Data")]
        public GetPlaylistResultTrack[] Tracks { get; set; } = Array.Empty<GetPlaylistResultTrack>();
    }
}
