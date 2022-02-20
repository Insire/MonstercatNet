using Refit;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public interface IMonstercatApi
    {
        /// <summary>
        /// authenticate the given HttpClient instance with the provided credentials
        /// </summary>
        [Post("/sign-in")]
        Task Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials, CancellationToken token = default);

        /// <summary>
        /// signout
        /// </summary>
        [Post("/sign-out")]
        Task Logout(CancellationToken token = default);

        /// <summary>
        /// 2FA Authorization
        /// </summary>
        [Post("/signin/token")]
        Task Login(string twoFactorAuthToken, CancellationToken token = default);

        /// <summary>
        /// resend 2FA Authorization
        /// </summary>
        [Post("/signin/token/resend")]
        Task Resend(string twoFactorAuthToken, CancellationToken token = default);

        /// <summary>
        /// fetch account info
        /// </summary>
        [Get("/me")]
        Task<Self> GetSelf(CancellationToken token = default);

        /// <summary>
        /// fetch available filters for track browsing
        /// </summary>
        [Get("/catalog/filters")]
        Task<TrackFilters> GetTrackSearchFilters(CancellationToken token = default);

        /// <summary>
        /// browse tracks
        /// </summary>
        [Get("/catalog/browse")]
        Task<TrackSearchResult> SearchTracks([Query(CollectionFormat = CollectionFormat.Csv)] TrackSearchRequest request, CancellationToken token = default);

        /// <summary>
        /// fetch all releases
        /// </summary>
        [Get("/releases")]
        Task<ReleaseBrowseResult> GetReleases([Query] ReleaseBrowseRequest request, CancellationToken token = default);

        /// <summary>
        /// fetch a specific release via its id
        /// </summary>
        /// <param name="catalogId"><see cref="Release.CatalogId"/></param>
        [Get("/catalog/release/{catalogId}")]
        Task<ReleaseResult> GetRelease([Query] string catalogId, CancellationToken token = default);

        /// <summary>
        /// returns as zip file as byte array
        /// </summary>
        [Get("/release/{request.ReleaseId}/download")]
        Task<HttpContent> DownloadRelease([Query] ReleaseDownloadRequest request, CancellationToken token = default);

        /// <summary>
        /// gold subscription required
        /// </summary>
        [Get("/release/{request.ReleaseId}/track-download/{request.TrackId}")]
        Task<HttpContent> DownloadTrack([Query] TrackDownloadRequest request, CancellationToken token = default);

        /// <summary>
        /// no login required
        /// </summary>
        [Get("/release/{request.ReleaseId}/track-stream/{request.TrackId}")]
        Task<HttpContent> StreamTrack([Query] TrackStreamRequest request, CancellationToken token = default);

        /// <summary>
        /// create a new playlist. requires login.
        /// </summary>
        [Post("/playlist")]
        Task<CreatePlaylistResult> CreatePlaylist(PlaylistCreateRequest request, CancellationToken token = default);

        /// <summary>
        /// delete a user specific playlist via its id
        /// </summary>
        /// <param name="playlistId"><see cref="Playlist.Id"/></param>
        [Post("/playlist/{playlistId}/delete")]
        Task DeletePlaylist([Query] Guid playlistId, CancellationToken token = default);

        /// <summary>
        /// fetch your playlists. requires login.
        /// </summary>
        [Get("/playlists")]
        Task<SelfPlaylistsResult> GetSelfPlaylists(CancellationToken token = default);

        /// <summary>
        /// fetch a playlist. Might require login, depending on whether you fetch your own playlist or the playlist is private.
        /// </summary>
        [Get("/playlist/{playlistId}")]
        Task<GetPlaylistResult> GetPlaylist([Query] Guid playlistId, CancellationToken token = default);

        /// <summary>
        /// add one track to the end of a playlist
        /// </summary>
        [Post("/playlist/{request.PlaylistId}/add")]
        Task PlaylistAddTrack(AddPlaylistTrackRequest request, CancellationToken token = default);

        /// <summary>
        /// deletes a specfic track from an existing playlist
        /// </summary>
        /// <remarks>
        /// requests to this endpoint might fail, if the track to be deleted was added only recently
        /// </remarks>
        [Post("/playlist/{request.PlaylistId}/remove")]
        Task PlaylistDeleteTrack(PlaylistDeleteTrackRequest request, CancellationToken token = default);

        /// <summary>
        /// Getting a playlist complete tracklist
        /// </summary>
        [Get("/playlist/{request.PlaylistId}/catalog")]
        Task<GetPlaylistTracksResult> GetPlaylistTracks([Query] GetPlaylistTracksRequest request, CancellationToken token = default);

        /// <summary>
        /// rename a playlist
        /// </summary>
        /// <remarks>
        /// this can a take a while
        /// </remarks>
        [Post("/playlist/{request.PlaylistId}")]
        Task<UpdatePlaylistResult> UpdatePlaylist(UpdatePlaylistRequest request, CancellationToken token = default);
    }
}
