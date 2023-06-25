using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class SelfPlaylists : ResultBase
    {
        public SelfPlaylist[] Data { get; set; } = Array.Empty<SelfPlaylist>();
        public Fields? Fields { get; set; }
        public int Count { get; set; }
    }
}
