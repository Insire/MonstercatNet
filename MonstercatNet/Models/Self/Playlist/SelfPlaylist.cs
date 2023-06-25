using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class SelfPlaylist
    {
        public bool Archived { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid Id { get; set; }

        public bool IsPublic { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public string TileFileId { get; set; } = string.Empty;

        public string BackgroundFileId { get; set; } = string.Empty;

        public bool MyLibrary { get; set; }

        public int NumRecords { get; set; }

        public PlaylistRecord[] Items { get; set; } = Array.Empty<PlaylistRecord>();
    }
}
