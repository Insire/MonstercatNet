using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public interface IMonstercatApi
    {
        /// <summary>
        /// authenticate the given HttpClient instance with the provided credentials
        /// </summary>
        [Post("/signin")]
        Task Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials);

        /// <summary>
        /// signout
        /// </summary>
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

        /// <summary>
        /// fetch account info
        /// </summary>
        [Get("/self")]
        Task<Self> GetSelf();

        /// <summary>
        /// fetch available filters for track browsing
        /// </summary>
        [Get("/catalog/filters")]
        Task<TrackFilters> GetTrackSearchFilters();

        /// <summary>
        /// browse tracks
        /// </summary>
        [Get("/catalog/browse")]
        Task<TrackSearchResult> SearchTracks([Query(CollectionFormat = CollectionFormat.Csv)] TrackSearchRequest request);

        /// <summary>
        /// fetch all releases
        /// </summary>
        [Get("/releases")]
        Task<ReleaseBrowseResult> GetReleases([Query] ReleaseBrowseRequest request);

        /// <summary>
        /// fetch a specific release via its id
        /// </summary>
        /// <param name="catalogId"><see cref="Release.CatalogId"/></param>
        [Get("/catalog/release/{catalogId}")]
        Task<ReleaseResult> GetRelease([Query] string catalogId);

        /// <summary>
        /// returns a jpg
        /// </summary>
        [Get("/release/{request.ReleaseId}/cover")]
        Task<HttpContent> GetReleaseCover([Query] ReleaseCoverRequest request);

        /// <summary>
        /// returns as zip file as byte array
        /// </summary>
        [Get("/release/{request.ReleaseId}/download")]
        Task<HttpContent> DownloadRelease([Query] ReleaseDownloadRequest request);

        /// <summary>
        /// gold subscription required
        /// </summary>
        [Get("/release/{request.ReleaseId}/track-download/{request.TrackId}")]
        Task<HttpContent> DownloadTrack([Query] TrackDownloadRequest request);

        [Get("/release/{request.ReleaseId}/track-stream/{request.TrackId}")]
        Task<HttpContent> StreamTrack([Query] TrackStreamRequest request);

        [Post("/self/playlist")]
        Task<Playlist> CreatePlaylist(PlaylistCreateRequest request);

        /// <summary>
        /// delete a user specific playlist via its id
        /// </summary>
        /// <param name="playlistId"><see cref="Playlist.Id"/></param>
        [Delete("/playlist/{playlistId}")]
        Task DeletePlaylist([Query] Guid playlistId);

        [Get("/self/playlists")]
        Task<SelfPlaylists> GetSelfPlaylists();

        [Patch("/playlist/{request.PlaylistId}/record")]
        Task PlaylistAddTrack(PlaylistAddTrackRequest request);

        // /playlist/[playlistId]

        // /playlist/[playlistId]/catalog
    }
}
