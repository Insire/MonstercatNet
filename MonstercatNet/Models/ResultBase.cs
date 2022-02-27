namespace SoftThorn.MonstercatNet
{
    public abstract class ResultBase
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
