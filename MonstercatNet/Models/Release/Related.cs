using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Related : ResultBase
    {
        public Release[] Results { get; set; } = Array.Empty<Release>();
    }
}
