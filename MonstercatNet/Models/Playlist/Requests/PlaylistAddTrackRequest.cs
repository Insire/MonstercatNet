using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistAddTrackRequest
    {
        public Guid TrackId { get; set; }
        public Guid ReleaseId { get; set; }

        public Guid PlaylistId { get; set; }
    }
}
