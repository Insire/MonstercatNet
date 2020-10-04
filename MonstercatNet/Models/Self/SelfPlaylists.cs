using System;

namespace SoftThorn.MonstercatNet
{
    public class SelfPlaylists
    {
        public Playlist[] Results { get; set; } = new Playlist[0];
        public bool Archived { get; set; }
        public int Count { get; set; }
        public string DefaultSorts { get; set; } = "null";
        public string LabelId { get; set; } = "";
        public int Limit { get; set; } = 100;
        public int Offset { get; set; }
        public string Search { get; set; } = "";
        public int Total { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Visibility { get; set; }
        public Fields Fields { get; set; } = new Fields();
    }
}
