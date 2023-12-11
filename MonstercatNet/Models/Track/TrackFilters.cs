using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class TrackFilters
    {
        public string[] Genres { get; set; } = Array.Empty<string>();
        public string[] Tags { get; set; } = Array.Empty<string>();

        /// <summary>
        /// release type, such as EP
        /// </summary>
        public string[] Types { get; set; } = Array.Empty<string>();
    }
}
