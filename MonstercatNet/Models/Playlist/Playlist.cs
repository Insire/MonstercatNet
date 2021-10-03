using Refit;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Playlist
    {
        [AliasAs("Archived")]
        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid Id { get; set; }
        [AliasAs("IsPublic")]
        public bool Public { get; set; }

        public bool MyLibrary { get; set; }
        public int NumRecords { get; set; }

        [AliasAs("Title")]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public Guid UserId { get; set; }
        [AliasAs("Items")]
        public PlaylistTrack[] Tracks { get; set; } = new PlaylistTrack[0];

        public string TileFileId { get; set; } = "";
        public string BackgroundFileId { get; set; } = "";
    }
}
