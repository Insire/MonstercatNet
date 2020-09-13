using Refit;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    // https://reactiveui.github.io/refit/
    public interface IMonstercatApi
    {
        [Post("/signin")]
        Task Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials);

        [Post("/signout")]
        Task Logout();

        /// <summary>
        /// 2FA Authorization
        /// </summary>
        [Post("/signin/token")]
        Task Login(string twoFactorAuthToken);

        /// <summary>
        /// resend 2FA Authorization
        /// </summary>
        [Post("/signin/token/resend")]
        Task Resend(string twoFactorAuthToken);

        [Get("/self")]
        Task<Self> GetSelf();

        //[Get("/self/playlists")]
        //Task<PlaylistBrowseResult> GetSelfPlaylists();

        //[Get("/releases")]
        //Task<ReleaseBrowseResult> GetReleases();

        [Get("/catalog/filters")]
        Task<TrackFilters> GetTrackSearchFilters();

        // https://connect.monstercat.com/v2/catalog/browse?type=EP;genre=Dance;tag=Dark
        [Get("/catalog/browse")]
        Task<TrackSearchResult> SearchTracks([Query(CollectionFormat = CollectionFormat.Csv)] TrackSearchRequest request);

        // /catalog/release/[catalogId]

        // /release/[releaseId]/cover

        // /release/[releaseId]/track-stream

        // /release/[releaseId]/download

        // /release/[releaseId]/track-download/[trackId]

        // /playlist/[playlistId]

        // /playlist/[playlistId]/catalog
    }
}
