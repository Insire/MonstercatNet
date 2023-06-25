namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistResultTrack : Track
    {
        public GetPlaylistResultFile? File { get; set; }

        public int PlaylistSort { get; set; }
    }
}
