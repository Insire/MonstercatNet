using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Artist
    {
        public Guid ArtistId { get; set; }
        public string Uri { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Public { get; set; }
        public string Role { get; set; } = string.Empty;

        public Guid ReleaseId { get; set; }
        public int ArtistNumber { get; set; }
        public string URI { get; set; } = string.Empty;
        public Guid? ProfileFileId { get; set; }
        public string Platform { get; set; } = string.Empty;
    }
}
