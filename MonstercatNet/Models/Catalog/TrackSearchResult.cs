namespace SoftThorn.MonstercatNet
{
    /// <summary>
    /// represents tracks in the catalog, in reverse chronological order (newest first)
    /// </summary>
    public sealed class TrackSearchResult
    {
        public Track[] Results { get; set; } = new Track[0] { };
        public object? NotFound { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Skip { get; set; }
    }
}
