namespace SoftThorn.MonstercatNet
{
    public sealed class PlaylistTracks : ResultBase
    {
        public Track[] Results { get; set; } = new Track[0];
        public object[] NotFound { get; set; } = new object[0];
    }
}
