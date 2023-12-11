using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class ReleaseResult
    {
        public Release? Release { get; set; }

        public Track[] Tracks { get; set; } = Array.Empty<Track>();
    }
}
