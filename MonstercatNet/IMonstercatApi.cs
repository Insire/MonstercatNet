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
        [Post("/sign-in")]
        Task Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials);

        /// <summary>
        /// signout
        /// </summary>
        [Post("/sign-out")]
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

        /// <summary>
        /// no login required
        /// </summary>
        [Get("/release/{request.ReleaseId}/track-stream/{request.TrackId}")]
        Task<HttpContent> StreamTrack([Query] TrackStreamRequest request);

        /// <summary>
        /// create a new playlist. requires login.
        /// </summary>
        [Post("/playlist")]
        Task<CreatePlaylistResponse> CreatePlaylist(PlaylistCreateRequest request);

        /// <summary>
        /// delete a user specific playlist via its id
        /// </summary>
        /// <param name="playlistId"><see cref="Playlist.Id"/></param>
        [Post("/playlist/{playlistId}/delete")]
        Task DeletePlaylist([Query] Guid playlistId);

        /// <summary>
        /// fetch your playlists. requires login.
        /// </summary>
        [Get("/playlists")]
        Task<SelfPlaylists> GetSelfPlaylists();

        /// <summary>
        /// fetch a playlist. Might require login, depending on whether you fetch your own playlist or the playlist is private.
        /// </summary>
        [Get("/playlist/{playlistId}")]
        Task<Playlist> GetPlaylist([Query] Guid playlistId);

        /// <summary>
        /// add one track to the end of a playlist
        /// </summary>
        [Patch("/playlist/{request.PlaylistId}/record")]
        Task PlaylistAddTrack(PlaylistAddTrackRequest request);

        /// <summary>
        /// deletes a specfic track from an existing playlist
        /// </summary>
        /// <remarks>
        /// requests to this endpoint might fail, if the track to be deleted was added only recently
        /// </remarks>
        [Delete("/playlist/{playlistId}/record")]
        Task PlaylistDeleteTrack(Guid playlistId, [Body] PlaylistDeleteTrackRequest request);

        /// <summary>
        /// Getting a playlist complete tracklist
        /// </summary>
        [Get("/playlist/{playlistId}/catalog")]
        Task<PlaylistTracks> GetPlaylistTracks([Query] Guid playlistId);

        /// <summary>
        /// rename a playlist
        /// </summary>
        /// <remarks>
        /// this can a take a while
        /// </remarks>
        [Post("/playlist/{playlistId}")]
        Task<Playlist> RenamePlaylist([Query] Guid playlistId, [Body] PlaylistRenameRequest request);

        /// <summary>
        /// switch the playlist from public to private or vice versa
        /// </summary>
        /// <remarks>
        /// this can a take a while
        /// </remarks>
        [Patch("/playlist/{playlistId}")]
        Task<Playlist> SwitchPlaylistAvailability([Query] Guid playlistId, [Body] PlaylistSwitchAvailabilityRequest request);
    }
}
