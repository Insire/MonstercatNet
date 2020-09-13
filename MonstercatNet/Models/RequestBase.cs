using Refit;

namespace SoftThorn.MonstercatNet
{
    public abstract class RequestBase
    {
        private const int MaxLimit = 50;
        private const int MinLimit = 1;

        private int _limit = MaxLimit;
        /// <summary>
        /// Value can be set to any value between 1 and 50
        /// </summary>
        [AliasAs("limit")]
        public int Limit
        {
            get => _limit; set
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

        private int _skip;
        [AliasAs("skip")]
        public int Skip
        {
            get => _skip;
            set
            {
                if (Skip < 0)
                {
                    _skip = 0;
                }
                else
                {
                    _skip = value;
                }
            }
        }
    }
}
