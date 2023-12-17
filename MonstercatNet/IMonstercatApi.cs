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
        Task<LoginResponse> Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials, CancellationToken token = default);

        /// <summary>
        /// signout
        /// </summary>
        [Post("/sign-out")]
        Task Logout(CancellationToken token = default);

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
        /// delete a specific playlist via its id
        /// </summary>
        /// <param name="playlistId"><see cref="Playlist.Id"/></param>
        [Post("/playlist/{playlistId}/delete")]
        Task DeletePlaylist([Query] Guid playlistId, CancellationToken token = default);

        /// <summary>
        /// fetch your playlists. requires login.
        /// </summary>
        [Get("/playlists?mylibrary=1")]
        Task<SelfPlaylistsResult> GetSelfPlaylists(CancellationToken token = default);

        /// <summary>
        /// fetch one of your playlists. requires login.
        /// </summary>
        [Get("/playlist/{playlistId}/catalog")]
        Task<GetPlaylistResult> GetPlaylist([Query] Guid playlistId, GetPlaylistRequest request, CancellationToken token = default);

        /// <summary>
        /// add one track to the end of a playlist
        /// </summary>
        [Post("/playlist/{playlistId}/modify-items?type=add")]
        Task PlaylistAddTrack(Guid playlistId, PlaylistAddTrackRequest request, CancellationToken token = default);

        /// <summary>
        /// deletes a specfic track from an existing playlist
        /// </summary>
        /// <remarks>
        /// requests to this endpoint might fail, if the track to be deleted was added only recently
        /// </remarks>
        [Post("/playlist/{playlistId}/modify-items?type=remove")]
        Task PlaylistDeleteTrack(Guid playlistId, PlaylistDeleteTrackRequest request, CancellationToken token = default);

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
