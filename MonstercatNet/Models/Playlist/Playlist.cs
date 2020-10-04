using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Playlist
    {
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid Id { get; set; }
        public bool Public { get; set; }
        public bool MyLibrary { get; set; }
        public int NumRecords { get; set; }
        public string Name { get; set; } = "";
        public Guid UserId { get; set; }
        public PlaylistTrack[] Tracks { get; set; } = new PlaylistTrack[0];
    }
}
