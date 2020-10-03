namespace SoftThorn.MonstercatNet.Models.Playlist
{
    public sealed class Playlist
    {
        public string Name { get; set; } = "";
        public bool Public { get; set; }
        public PlaylistTrack[] Tracks { get; set; } = new PlaylistTrack[0];
    }
}
