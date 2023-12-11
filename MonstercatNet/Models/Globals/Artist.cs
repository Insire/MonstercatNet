using System;
using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class Artist
    {
        public Guid ArtistId { get; set; }

        [JsonPropertyName("URI")]
        public string Uri { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool Public { get; set; }

        public string Role { get; set; } = string.Empty;

        public Guid ReleaseId { get; set; }

        public int ArtistNumber { get; set; }

        public Guid? ProfileFileId { get; set; }

        public string Platform { get; set; } = string.Empty;

        public object? SquareFileId { get; set; }
    }
}
