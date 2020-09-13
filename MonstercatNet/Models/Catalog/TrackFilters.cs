namespace SoftThorn.MonstercatNet
{
    public sealed class TrackFilters
    {
        public string[] Genres { get; set; } = new string[0] { };
        public string[] Tags { get; set; } = new string[0] { };

        /// <summary>
        /// release type, such as EP
        /// </summary>
        public string[] Types { get; set; } = new string[0] { };
    }
}
