using Refit;

namespace SoftThorn.MonstercatNet
{
    public sealed class GetPlaylistRequest : RequestBase
    {
        [AliasAs("sort")]
        public string Sort { get; set; } = "playlist";

        [AliasAs("creatorfriendly")]
        public bool? Creatorfriendly { get; set; } = true;

        [AliasAs("nogold")]
        public bool? NoGold { get; set; } = true;

        [AliasAs("streamerMode")]
        public bool? StreamerMode { get; set; } = false;
    }
}
