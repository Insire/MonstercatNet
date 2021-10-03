using Refit;

namespace SoftThorn.MonstercatNet
{
    public abstract class RequestBase
    {
        internal const int MaxLimit = 50;
        internal const int MinLimit = 1;
        internal const int MinSkip = 0;

        private int _limit = MaxLimit;
        /// <summary>
        /// Value can be set to any value between 1 and 50
        /// </summary>
        [AliasAs("limit")]
        public int Limit
        {
            get { return _limit; }
            set
            {
                if (value > MaxLimit)
                {
                    _limit = MaxLimit;
                }
                else
                {
                    if (value < MinLimit)
                    {
                        _limit = MinLimit;
                    }
                    else
                    {
                        _limit = value;
                    }
                }
            }
        }

        private int _skip = MinSkip;
        [AliasAs("Offset")]
        public int Skip
        {
            get { return _skip; }
            set
            {
                if (value < MinSkip)
                {
                    _skip = MinSkip;
                }
                else
                {
                    _skip = value;
                }
            }
        }
    }
}
