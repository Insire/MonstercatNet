using System;

namespace SoftThorn.MonstercatNet
{
    public class Catalog
    {
        public string artistsTitle { get; set; }
        public int bpm { get; set; }
        public bool creatorFriendly { get; set; }
        public DateTime debutDate { get; set; }
        public bool downloadable { get; set; }
        public int duration { get; set; }
        public bool _explicit { get; set; }
        public string genrePrimary { get; set; }
        public string genreSecondary { get; set; }
        public string id { get; set; }
        public bool inEarlyAccess { get; set; }
        public string isrc { get; set; }
        public bool streamable { get; set; }
        public string title { get; set; }
        public int trackNumber { get; set; }
        public string[] tags { get; set; }
        public string version { get; set; }
        public Release release { get; set; }
        public Artist[] artists { get; set; }
        public int playlistSort { get; set; }
    }
}
