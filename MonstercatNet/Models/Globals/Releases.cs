using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Releases : ResultBase
    {
        public Release[] Data { get; set; } = Array.Empty<Release>();
    }
}
