using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistResultRelease
    {
        public string ArtistsTitle { get; set; } = string.Empty;

        public string CatalogId { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid Id { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ReleaseDateTimezone { get; set; } = string.Empty;

        public string[] Tags { get; set; } = Array.Empty<string>();

        public string Title { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Universal Product Code code of the release
        /// </summary>
        public string UPC { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;
    }
}
