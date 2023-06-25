namespace SoftThorn.MonstercatNet
{
    public sealed class UserSettings
    {
        public bool HideUnlicensableTracks { get; set; }
        public bool BlockUnlicensableTracks { get; set; }
        public bool PlaylistPublicDefault { get; set; }
        public string PreferredFormat { get; set; } = string.Empty;
        public bool AutoEnableStreamerMode { get; set; }
    }
}
