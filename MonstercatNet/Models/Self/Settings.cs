namespace SoftThorn.MonstercatNet
{
    public sealed class Settings
    {
        public string Id { get; set; } = string.Empty;
        public bool HideNonLicensableTracks { get; set; }
        public bool BlockNonLicensableTracks { get; set; }
        public bool PlaylistPublicByDefault { get; set; }
        public string PreferredDownloadFormat { get; set; } = string.Empty;
    }
}
