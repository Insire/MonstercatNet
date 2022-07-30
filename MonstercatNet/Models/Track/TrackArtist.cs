using Newtonsoft.Json;
using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class TrackArtist
    {
        public Guid Id { get; set; }

        [JsonProperty("URI")]
        public string Uri { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool Public { get; set; }

        public string Role { get; set; } = string.Empty;

        public Guid? ProfileFileId { get; set; }

        public Guid? CatalogRecordId { get; set; }
    }
}
