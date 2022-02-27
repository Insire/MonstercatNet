using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistAddTrackRequest
    {
        public PlaylistRecord[] Records { get; set; } = Array.Empty<PlaylistRecord>();
    }
}
