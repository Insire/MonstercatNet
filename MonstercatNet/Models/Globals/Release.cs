using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Release
    {
        public string AlbumNotes { get; set; } = string.Empty;
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

        public ReleaseLink[]? Links { get; set; }

        public string BrandId { get; set; } = string.Empty;
        public string BrandTitle { get; set; } = string.Empty;
        public string YoutubeUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[]? Tags { get; set; }

        public Artist[]? Artists { get; set; }

        public DateTime CacheTime { get; set; }
        public string CacheStatus { get; set; } = string.Empty;
        public string CacheStatusDetail { get; set; } = string.Empty;

        public string CopyrightPLine { get; set; } = string.Empty;
        public string CoverFileId { get; set; } = string.Empty;

        public string FeaturedArtistsTitle { get; set; } = string.Empty;
        public string GRid { get; set; } = string.Empty;
        public DateTime? PrereleaseDate { get; set; }
        public DateTime? PresaveDate { get; set; }
        public string ReleaseDateTimezone { get; set; } = string.Empty;
        public object? SpotifyId { get; set; }

        /// <summary>
        /// Universal Product Code code of the release
        /// </summary>
        public string UPC { get; set; } = string.Empty;

        public string YouTubeUrl { get; set; } = string.Empty;
        public object? Tracks { get; set; }
    }
}
