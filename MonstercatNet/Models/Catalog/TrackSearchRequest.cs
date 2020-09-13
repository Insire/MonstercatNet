using Refit;

namespace SoftThorn.MonstercatNet
{
    public sealed class TrackSearchRequest : RequestBase
    {
        [AliasAs("genres")]
        public string[]? Genres { get; set; }

        [AliasAs("tags")]
        public string[]? Tags { get; set; }

        /// <summary>
        /// e.g. EP, Single, Album
        /// </summary>
        [AliasAs("types")]
        public string[]? ReleaseTypes { get; set; }

        /// <summary>
        /// Content Creator Status [Licenseable|Unlicensable]
        /// </summary>
        /// <remarks>
        /// true by default, because there are more tracks that are creatorfriendly than unfriendly, so it doesnt make sense to exclude them by default
        /// </remarks>
        [AliasAs("creatorfriendly")]
        public bool Creatorfriendly { get; set; } = true;
    }
}
