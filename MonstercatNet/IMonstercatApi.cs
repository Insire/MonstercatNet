using Refit;
using System;
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

        [Get("/catalog/filters")]
        Task<TrackFilters> GetTrackSearchFilters();

        [Get("/catalog/browse")]
        Task<TrackSearchResult> SearchTracks([Query(CollectionFormat = CollectionFormat.Csv)] TrackSearchRequest request);

        [Get("/releases")]
        Task<ReleaseBrowseResult> GetReleases([Query] ReleaseBrowseRequest request);

        [Get("/catalog/release/{catalogId}")]
        Task<ReleaseResult> GetRelease([Query] string catalogId);

        // /release/[releaseId]/cover

        // /release/[releaseId]/track-stream

        // /release/[releaseId]/download

        // /release/[releaseId]/track-download/[trackId]

        // /playlist/[playlistId]

        // /playlist/[playlistId]/catalog

        //[Get("/self/playlists")]
        //Task<PlaylistBrowseResult> GetSelfPlaylists();
    }
}
