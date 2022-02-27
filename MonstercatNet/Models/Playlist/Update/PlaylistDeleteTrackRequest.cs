using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistDeleteTrackRequest
    {
        public PlaylistRecord[] Records { get; set; } = Array.Empty<PlaylistRecord>();
    }
}
