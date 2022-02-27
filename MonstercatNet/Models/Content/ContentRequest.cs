#nullable disable

using Refit;

namespace SoftThorn.MonstercatNet
{
    public class ContentRequest
    {
        [AliasAs("encoding")]
        public string Encoding { get; set; }

        [AliasAs("url")]
        public string Url { get; set; }

        [AliasAs("width")]
        public int Width { get; set; }
    }
}
