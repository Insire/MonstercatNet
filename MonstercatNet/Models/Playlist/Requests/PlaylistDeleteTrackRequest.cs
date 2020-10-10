using Refit;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistDeleteTrackRequest
    {
        [AliasAs("trackId")]
        public Guid TrackId { get; set; }

        [AliasAs("releaseId")]
        public Guid ReleaseId { get; set; }

        [AliasAs("sort")]
        public int Sort { get; set; }
    }
}
