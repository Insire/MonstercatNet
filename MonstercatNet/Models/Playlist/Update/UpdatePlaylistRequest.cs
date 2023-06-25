using System;
using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class UpdatePlaylistRequest
    {
        public string? Title { get; set; } = string.Empty;

        public bool Archived { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("Id")]
        public Guid PlaylistId { get; set; }

        public bool? IsPublic { get; set; }

        public string? Description { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public string TileFileId { get; set; } = string.Empty;

        public string BackgroundFileId { get; set; } = string.Empty;

        public bool MyLibrary { get; set; }

        public int NumRecords { get; set; }

        public object? Items { get; set; }
    }
}
