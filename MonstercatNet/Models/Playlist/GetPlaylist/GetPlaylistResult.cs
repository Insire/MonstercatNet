using Refit;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistResult
    {
        public bool Archived { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid Id { get; set; }

        public bool IsPublic { get; set; }

        public bool MyLibrary { get; set; }
        public int NumRecords { get; set; }

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public Guid UserId { get; set; }

        [AliasAs("Items")]
        public GetPlaylistResultTrack[] Tracks { get; set; } = Array.Empty<GetPlaylistResultTrack>();

        public string TileFileId { get; set; } = "";
        public string BackgroundFileId { get; set; } = "";
    }
}
