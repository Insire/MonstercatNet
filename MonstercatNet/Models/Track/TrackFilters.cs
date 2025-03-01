namespace SoftThorn.MonstercatNet
{
    public sealed class TrackFilters
    {
        public Brand[] Brands { get; set; } = Array.Empty<Brand>();

        public string[] Genres { get; set; } = Array.Empty<string>();

        /// <summary>
        /// release type, such as EP
        /// </summary>
        public string[] Types { get; set; } = Array.Empty<string>();
    }

    public sealed class Brand
    {
        public bool Active { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
