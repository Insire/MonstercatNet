using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public sealed class MonstercatApi : IMonstercatApi
    {
        private static readonly RefitSettings _settings = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer()
        };

        /// <summary>
        /// Generate the client to be able to interact with the monstercat api
        /// </summary>
        /// <param name="client">the httpclient to use for all requests</param>
        /// <remarks>
        /// the httpclient stores an sid as cookie, so the instance should be reused for all requests to the api
        /// </remarks>
        public static IMonstercatApi Create(HttpClient client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return new MonstercatApi(RestService.For<IMonstercatApi>(client, _settings));
        }

        private readonly IMonstercatApi _service;

        private MonstercatApi(IMonstercatApi service)
        {
            _service = service;
        }

        public Task Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials)
        {
            if (credentials is null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }

            if (credentials.Password is null)
            {
                throw new ArgumentNullException(nameof(ApiCredentials.Password));
            }

            if (credentials.Email is null)
            {
                throw new ArgumentNullException(nameof(ApiCredentials.Email));
            }

            if (credentials.Password.Length == 0)
            {
                throw new ArgumentNullException(nameof(ApiCredentials.Password));
            }

            if (credentials.Email.Length == 0)
            {
                throw new ArgumentNullException(nameof(ApiCredentials.Email));
            }

            return _service.Login(credentials);
        }

        public Task Logout()
        {
            return _service.Logout();
        }

        public Task Login(string twoFactorAuthToken)
        {
            if (twoFactorAuthToken is null)
            {
                throw new ArgumentNullException(nameof(twoFactorAuthToken));
            }

            if (twoFactorAuthToken.Length == 0)
            {
                throw new ArgumentNullException(nameof(twoFactorAuthToken));
            }

            return _service.Login(twoFactorAuthToken);
        }

        public Task Resend(string twoFactorAuthToken)
        {
            if (twoFactorAuthToken is null)
            {
                throw new ArgumentNullException(nameof(twoFactorAuthToken));
            }

            if (twoFactorAuthToken.Length == 0)
            {
                throw new ArgumentNullException(nameof(twoFactorAuthToken));
            }

            return _service.Resend(twoFactorAuthToken);
        }

        public Task<Self> GetSelf()
        {
            return _service.GetSelf();
        }

        public Task<TrackFilters> GetTrackSearchFilters()
        {
            return _service.GetTrackSearchFilters();
        }

        /// <summary>
        /// return tracks in the catalog, in reverse chronological order (newest first)
        /// </summary>
        public Task<TrackSearchResult> SearchTracks(TrackSearchRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return _service.SearchTracks(request);
        }

        public Task<ReleaseBrowseResult> GetReleases(ReleaseBrowseRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return _service.GetReleases(request);
        }

        public Task<ReleaseResult> GetRelease(string catalogId)
        {
            if (catalogId is null)
            {
                throw new ArgumentNullException(nameof(catalogId));
            }

            if (catalogId.Length == 0)
            {
                throw new ArgumentNullException(nameof(catalogId));
            }

            return _service.GetRelease(catalogId);
        }

        public Task<HttpContent> GetReleaseCover(ReleaseCoverRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ReleaseId == Guid.Empty)
            {
                throw new ArgumentException(nameof(ReleaseCoverRequest.ReleaseId));
            }

            return _service.GetReleaseCover(request);
        }

        /// <summary>
        /// gold membership required
        /// </summary>
        public Task<HttpContent> DownloadRelease(ReleaseDownloadRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ReleaseId == Guid.Empty)
            {
                throw new ArgumentException(nameof(ReleaseDownloadRequest.ReleaseId));
            }

            return _service.DownloadRelease(request);
        }

        /// <summary>
        /// gold membership required
        /// </summary>
        public Task<HttpContent> DownloadTrack(TrackDownloadRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ReleaseId == Guid.Empty)
            {
                throw new ArgumentException(nameof(TrackDownloadRequest.ReleaseId));
            }

            if (request.TrackId == Guid.Empty)
            {
                throw new ArgumentException(nameof(TrackDownloadRequest.TrackId));
            }

            return _service.DownloadTrack(request);
        }

        public Task<HttpContent> StreamTrack(TrackStreamRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ReleaseId == Guid.Empty)
            {
                throw new ArgumentException(nameof(TrackDownloadRequest.ReleaseId));
            }

            if (request.TrackId == Guid.Empty)
            {
                throw new ArgumentException(nameof(TrackDownloadRequest.TrackId));
            }

            return _service.StreamTrack(request);
        }

        public Task<CreatePlaylistResponse> CreatePlaylist(PlaylistCreateRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Title == null)
            {
                throw new ArgumentNullException(nameof(request.Title));
            }

            return _service.CreatePlaylist(request);
        }

        public Task DeletePlaylist(Guid playlistId)
        {
            if (playlistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playlistId));
            }

            return _service.DeletePlaylist(playlistId);
        }

        public Task<SelfPlaylistsResult> GetSelfPlaylists()
        {
            return _service.GetSelfPlaylists();
        }

        public Task PlaylistAddTrack(PlaylistAddTrackRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.PlaylistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(request.PlaylistId));
            }

            if (request.ReleaseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(request.ReleaseId));
            }

            if (request.TrackId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(request.TrackId));
            }

            return _service.PlaylistAddTrack(request);
        }

        public Task PlaylistDeleteTrack(PlaylistDeleteTrackRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.PlaylistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(request.PlaylistId));
            }

            if (request.ReleaseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(request.ReleaseId));
            }

            if (request.TrackId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(request.TrackId));
            }

            return _service.PlaylistDeleteTrack(request);
        }

        public Task<PlaylistTracks> GetPlaylistTracks(Guid playlistId)
        {
            if (playlistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playlistId));
            }

            return _service.GetPlaylistTracks(playlistId);
        }

        public Task<Playlist> GetPlaylist(Guid playlistId)
        {
            if (playlistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playlistId));
            }

            return _service.GetPlaylist(playlistId);
        }

        public Task<Playlist> UpdatePlaylist(PlaylistUpdateRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.PlaylistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(request.PlaylistId));
            }

            if (request.UpdatedAt == DateTime.MinValue)
            {
                throw new ArgumentNullException(nameof(request.UpdatedAt));
            }

            return _service.UpdatePlaylist(request);
        }
    }
}
