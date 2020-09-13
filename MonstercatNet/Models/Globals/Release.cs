using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Release
    {
        public string ArtistsTitle { get; set; } = string.Empty;
        public string CatalogId { get; set; } = string.Empty;
        public bool Downloadable { get; set; }
        public string GenrePrimary { get; set; } = string.Empty;
        public string GenreSecondary { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public bool InEarlyAccess { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool Streamable { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public object[]? Links { get; set; }
    }
}
