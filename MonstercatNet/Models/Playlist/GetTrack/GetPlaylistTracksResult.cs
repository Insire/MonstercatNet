using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistTracksResult : ResultBase
    {
        public GetPlaylistTracksResultTrack[] Data { get; set; } = Array.Empty<GetPlaylistTracksResultTrack>();

        public object[]? NotFound { get; set; }
    }
}
