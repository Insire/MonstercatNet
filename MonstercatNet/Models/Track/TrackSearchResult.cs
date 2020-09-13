namespace SoftThorn.MonstercatNet
{
    /// <summary>
    /// represents tracks in the catalog, in reverse chronological order (newest first)
    /// </summary>
    public sealed class TrackSearchResult : ResultBase
    {
        public Track[] Results { get; set; } = new Track[0] { };
        public object? NotFound { get; set; }
    }
}
