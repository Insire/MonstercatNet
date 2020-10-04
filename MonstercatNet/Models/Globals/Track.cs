using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Track
    {
        public string ArtistsTitle { get; set; } = string.Empty;
        public int Bpm { get; set; }
        public bool CreatorFriendly { get; set; }
        public DateTime DebutDate { get; set; }
        public bool Downloadable { get; set; }
        public int Duration { get; set; }
        public bool Explicit { get; set; }
        public string GenrePrimary { get; set; } = string.Empty;
        public string GenreSecondary { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public bool InEarlyAccess { get; set; }
        public string Isrc { get; set; } = string.Empty;
        public bool Streamable { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TrackNumber { get; set; }
        public string[]? Tags { get; set; }
        public string Version { get; set; } = string.Empty;
        public TrackRelease Release { get; set; } = new TrackRelease();
        public Artist[]? Artists { get; set; }
        public int PlaylistSort { get; set; }
    }
}
