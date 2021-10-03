using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class TrackRelease
    {
        public string ArtistsTitle { get; set; } = string.Empty;
        public string CatalogId { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string[]? Tags { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Version { get; set; } = string.Empty;
        public string Upc { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
