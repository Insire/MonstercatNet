using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistCreateRequest
    {
        public string Name { get; set; } = "";
        public bool Public { get; set; }
        public PlaylistCreateTrack[] Tracks { get; set; } = new PlaylistCreateTrack[0];
        public Guid Id { get; set; } = Guid.Empty;
    }

    public sealed class PlaylistCreateTrack
    {
        public Guid TrackId { get; set; }
        public Guid ReleaseId { get; set; }
    }
}
