using Refit;

namespace SoftThorn.MonstercatNet
{
    public abstract class ResultBase
    {
        public int Total { get; set; }
        public int Limit { get; set; }

        [AliasAs("Offset")]
        public int Skip { get; set; }
    }
}
