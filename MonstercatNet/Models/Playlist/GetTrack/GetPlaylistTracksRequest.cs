using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistTracksRequest : RequestBase
    {
        public Guid PlaylistId { get; set; }
    }
}
