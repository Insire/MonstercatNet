using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistDeleteTrackRequest
    {
        public Guid TrackId { get; set; }

        public Guid ReleaseId { get; set; }
        public Guid PlaylistId { get; set; }

        public int Sort { get; set; } = 0;
    }
}
