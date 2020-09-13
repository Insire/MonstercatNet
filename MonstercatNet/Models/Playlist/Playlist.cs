using System;

namespace SoftThorn.MonstercatNet
{
    public class Playlist
    {
        public bool deleted { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string id { get; set; }
        public bool _public { get; set; }
        public bool myLibrary { get; set; }
        public int numRecords { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public object tracks { get; set; }
    }
}
