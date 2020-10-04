using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Playlist
    {
        public string Name { get; set; } = "";
        public bool Public { get; set; }
        public PlaylistTrack[] Tracks { get; set; } = new PlaylistTrack[0];
        public Guid Id { get; set; } = Guid.Empty;
    }
}
