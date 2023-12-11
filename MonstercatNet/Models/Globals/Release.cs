using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Release
    {
        public string AlbumNotes { get; set; } = string.Empty;

        public Artist[] Artists { get; set; } = Array.Empty<Artist>();

        public string ArtistsTitle { get; set; } = string.Empty;

        public int BrandId { get; set; }

        public string BrandTitle { get; set; } = string.Empty;

        public string CacheStatus { get; set; } = string.Empty;

        public string CacheStatusDetail { get; set; } = string.Empty;

        public DateTime CacheTime { get; set; }

        public string CatalogId { get; set; } = string.Empty;

        public string CopyrightPLine { get; set; } = string.Empty;

        public Guid CoverFileId { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool Downloadable { get; set; }

        public string FeaturedArtistsTitle { get; set; } = string.Empty;

        public string GenrePrimary { get; set; } = string.Empty;

        public string GenreSecondary { get; set; } = string.Empty;

        public string GRid { get; set; } = string.Empty;

        public Guid Id { get; set; }

        public bool InEarlyAccess { get; set; }

        public Link[] Links { get; set; } = Array.Empty<Link>();

        public object? PrereleaseDate { get; set; }

        public object? PresaveDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ReleaseDateTimezone { get; set; } = string.Empty;

        public object? SpotifyId { get; set; }

        public bool Streamable { get; set; }

        public string[] Tags { get; set; } = Array.Empty<string>();

        public string Title { get; set; } = string.Empty;

        public object? Tracks { get; set; }

        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Universal Product Code code of the release
        /// </summary>
        public string UPC { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public string YouTubeUrl { get; set; } = string.Empty;
    }
}
