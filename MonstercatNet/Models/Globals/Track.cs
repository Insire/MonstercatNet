using Newtonsoft.Json;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Track
    {
        [JsonProperty("ArtistsTitle")]
        public string ArtistsTitle { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public int BrandId { get; set; }

        [JsonProperty("BPM")]
        public decimal Bpm { get; set; }

        public bool CreatorFriendly { get; set; }
        public DateTime DebutDate { get; set; }


        public int Duration { get; set; }

        public bool Explicit { get; set; }

        public string GenrePrimary { get; set; } = string.Empty;

        public string GenreSecondary { get; set; } = string.Empty;

        public Guid Id { get; set; }

        /// <summary>
        /// Global Release Identifier of the release
        /// </summary>
        [JsonProperty("ISRC")]
        public string Isrc { get; set; } = string.Empty;

        public string LockStatus { get; set; } = string.Empty;

        public bool Public { get; set; }

        public string Title { get; set; } = string.Empty;

        public int TrackNumber { get; set; }

        public string[]? Tags { get; set; }

        public string Version { get; set; } = string.Empty;

        public TrackRelease Release { get; set; } = new TrackRelease();

        public TrackArtist[]? Artists { get; set; }

        public bool Downloadable { get; set; }

        public bool InEarlyAccess { get; set; }

        public bool Streamable { get; set; }
    }
}
