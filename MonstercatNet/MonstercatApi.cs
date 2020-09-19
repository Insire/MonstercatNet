using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public sealed class MonstercatApi : IMonstercatApi
    {
        private const string BaseUrl = "https://connect.monstercat.com/v2/";

        private static readonly RefitSettings _settings = new RefitSettings(new NewtonsoftJsonContentSerializer(), new DefaultUrlParameterFormatter(), new DefaultFormUrlEncodedParameterFormatter());

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

            client.BaseAddress = new Uri(BaseUrl);

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

            return _service.DownloadRelease(request);
        }

        /// <summary>
        /// gold membership required
        /// </summary>
        public Task<HttpContent> DownloadTrack([Query] TrackDownloadRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return _service.DownloadTrack(request);
        }
    }
}
