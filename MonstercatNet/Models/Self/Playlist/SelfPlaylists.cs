using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class SelfPlaylists : ResultBase
    {
        public SelfPlaylist[] Data { get; set; } = Array.Empty<SelfPlaylist>();
        public Fields Fields { get; set; } = new Fields();
        public int Count { get; set; }
    }
}
