namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistResultTrack
    {
        public string PlaylistId { get; set; } = "";
        public string ReleaseId { get; set; } = "";
        public string TrackId { get; set; } = "";
        public int Sort { get; set; }
    }
}
