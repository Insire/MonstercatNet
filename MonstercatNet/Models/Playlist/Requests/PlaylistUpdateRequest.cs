using Newtonsoft.Json;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistUpdateRequest
    {
        public string? Title { get; set; } = "";

        public bool Archived { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("Id")]
        public Guid PlaylistId { get; set; }

        public bool? IsPublic { get; set; }

        public string? Description { get; set; } = "";

        public Guid UserId { get; set; }

        public string TileFileId { get; set; } = "";

        public string BackgroundFileId { get; set; } = "";

        public bool MyLibrary { get; set; }
        public int NumRecords { get; set; }
        public object? Items { get; set; }
    }
}
