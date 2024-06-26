using Refit;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public sealed class MonstercatApi : MonstercatBase, IMonstercatApi
    {
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

            return new MonstercatApi(RestService.For<IMonstercatApi>(client, Settings));
        }

        public static IMonstercatApi Create()
        {
            var httpClient = new HttpClient(new HttpClientCookieHandler(NullCookieProcessor.Instance, ApiUriProvider.Instance)).UseMonstercatApiV2();

            return new MonstercatApi(RestService.For<IMonstercatApi>(httpClient, Settings));
        }

        private readonly IMonstercatApi _service;

        private MonstercatApi(IMonstercatApi service)
        {
            _service = service;
        }

        public Task<LoginResponse> Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials, CancellationToken token = default)
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

            return _service.Login(credentials, token);
        }

        public Task Logout(CancellationToken token = default)
        {
            return _service.Logout(token);
        }

        public Task<Self> GetSelf(CancellationToken token = default)
        {
            return _service.GetSelf(token);
        }

        public Task<TrackFilters> GetTrackSearchFilters(CancellationToken token = default)
        {
            return _service.GetTrackSearchFilters(token);
        }

        /// <summary>
        /// return tracks in the catalog, in reverse chronological order (newest first)
        /// </summary>
        public Task<TrackSearchResult> SearchTracks(TrackSearchRequest request, CancellationToken token = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return _service.SearchTracks(request, token);
        }

        public Task<ReleaseBrowseResult> GetReleases(ReleaseBrowseRequest request, CancellationToken token = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return _service.GetReleases(request, token);
        }

        public Task<ReleaseResult> GetRelease(string catalogId, CancellationToken token = default)
        {
            if (catalogId is null)
            {
                throw new ArgumentNullException(nameof(catalogId));
            }

            if (catalogId.Length == 0)
            {
                throw new ArgumentNullException(nameof(catalogId));
            }

            return _service.GetRelease(catalogId, token);
        }

        /// <summary>
        /// gold membership required
        /// </summary>
        public Task<HttpContent> DownloadTrack(TrackDownloadRequest request, CancellationToken token = default)
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

            return _service.DownloadTrack(request, token);
        }

        public Task<HttpContent> StreamTrack(TrackStreamRequest request, CancellationToken token = default)
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

            return _service.StreamTrack(request, token);
        }

        public Task<CreatePlaylistResult> CreatePlaylist(PlaylistCreateRequest request, CancellationToken token = default)
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

        public Task DeletePlaylist(Guid playlistId, CancellationToken token = default)
        {
            if (playlistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playlistId));
            }

            return _service.DeletePlaylist(playlistId, token);
        }

        public Task<SelfPlaylistsResult> GetSelfPlaylists(CancellationToken token = default)
        {
            return _service.GetSelfPlaylists(token);
        }

        public Task PlaylistAddTrack(Guid playlistId, PlaylistAddTrackRequest request, CancellationToken token = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (playlistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playlistId));
            }

            foreach (var record in request.Records)
            {
                if (record.PlaylistId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(record.PlaylistId));
                }

                if (record.ReleaseId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(record.ReleaseId));
                }

                if (record.TrackId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(record.TrackId));
                }
            }

            return _service.PlaylistAddTrack(playlistId, request, token);
        }

        public Task PlaylistDeleteTrack(Guid playlistId, PlaylistDeleteTrackRequest request, CancellationToken token = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (playlistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playlistId));
            }

            foreach (var record in request.Records)
            {
                if (record.PlaylistId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(record.PlaylistId));
                }

                if (record.ReleaseId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(record.ReleaseId));
                }

                if (record.TrackId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(record.TrackId));
                }
            }

            return _service.PlaylistDeleteTrack(playlistId, request, token);
        }

        public Task<GetPlaylistResult> GetPlaylist(Guid playlistId, GetPlaylistRequest request, CancellationToken token = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (playlistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playlistId));
            }

            return _service.GetPlaylist(playlistId, request, token);
        }

        public Task<UpdatePlaylistResult> UpdatePlaylist(UpdatePlaylistRequest request, CancellationToken token = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.PlaylistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(request.PlaylistId));
            }

            return _service.UpdatePlaylist(request, token);
        }
    }
}
