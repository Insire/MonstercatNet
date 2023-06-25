using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistResultArtist
    {
        public Guid CatalogRecordId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid ProfileFileId { get; set; }

        public bool Public { get; set; }

        public string Role { get; set; } = string.Empty;

        public string URI { get; set; } = string.Empty;
    }
}
