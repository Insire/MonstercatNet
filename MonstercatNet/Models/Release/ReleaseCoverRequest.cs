using Refit;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class ReleaseCoverRequest
    {
        [AliasAs("image_width")]
        public int Width { get; set; } = 512;

        public Guid ReleaseId { get; set; }
    }
}
