using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Artist
    {
        public Guid Id { get; set; }
        public string Uri { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Public { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
