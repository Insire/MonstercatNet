using Refit;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
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
        Task Login2FA(string token);

        /// <summary>
        /// resend 2FA Authorization
        /// </summary>
        [Post("/signin/token/resend")]
        Task Resend2FA(string token);

        [Get("/self")]
        Task<Self> GetSelf();

        [Get("/self/playlists")]
        Task<PlaylistBrowseResult> GetSelfPlaylists();

        [Get("/releases")]
        Task<ReleaseBrowseResult> GetReleases();

        // /catalog/browse

        // /catalog/filters

        // /catalog/release/[catalogId]

        // /release/[releaseId]/cover

        // /release/[releaseId]/track-stream

        // /release/[releaseId]/download

        // /release/[releaseId]/track-download/[trackId]

        // /playlist/[playlistId]

        // /playlist/[playlistId]/catalog
    }
}
