using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class TrackStreamRequest
    {
        public Guid ReleaseId { get; set; }

        public Guid TrackId { get; set; }

        public TrackStreamRequest()
        {
            ReleaseId = Guid.Empty;
            TrackId = Guid.Empty;
        }
    }
}
