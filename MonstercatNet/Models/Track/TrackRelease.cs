using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class TrackRelease
    {
        public string ArtistsTitle { get; set; } = string.Empty;

        public string CatalogId { get; set; } = string.Empty;

        public Guid Id { get; set; }

        public string[] Tags { get; set; } = Array.Empty<string>();

        public string Title { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public string ReleaseDateTimezone { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Universal Product Code code of the release
        /// </summary>
        [JsonPropertyName("UPC")]
        public string Upc { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
