using Refit;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistRenameRequest
    {
        [AliasAs("Title")]
        public string Name { get; set; } = "";

        public bool Archived { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid Id { get; set; }
        public bool IsPublic { get; set; }

        public string Description { get; set; } = "";

        public Guid UserId { get; set; }

        public string TileFileId { get; set; } = "";

        public string BackgroundFileId { get; set; } = "";

        public bool MyLibrary { get; set; }
        public int NumRecords { get; set; }
        public object? Items { get; set; }
    }
}
