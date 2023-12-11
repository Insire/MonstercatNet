using System;
using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public class Track
    {
        public TrackArtist[] Artists { get; set; } = Array.Empty<TrackArtist>();

        public string ArtistsTitle { get; set; } = string.Empty;

        [JsonPropertyName("BPM")]
        public decimal Bpm { get; set; }

        public string Brand { get; set; } = string.Empty;

        public int BrandId { get; set; }

        public bool CreatorFriendly { get; set; }

        public DateTime? DebutDate { get; set; }

        public bool Downloadable { get; set; }

        public int Duration { get; set; }

        public bool Explicit { get; set; }

        public string GenrePrimary { get; set; } = string.Empty;

        public string GenreSecondary { get; set; } = string.Empty;

        public Guid Id { get; set; }

        public bool InEarlyAccess { get; set; }
        /// <summary>
        /// Global Release Identifier of the release
        /// </summary>
        [JsonPropertyName("ISRC")]
        public string Isrc { get; set; } = string.Empty;

        public string LockStatus { get; set; } = string.Empty;

        public bool Public { get; set; }

        public TrackRelease? Release { get; set; }

        public bool Streamable { get; set; }

        public string[] Tags { get; set; } = Array.Empty<string>();

        public string Title { get; set; } = string.Empty;

        public int TrackNumber { get; set; }

        public string Version { get; set; } = string.Empty;
    }
}
