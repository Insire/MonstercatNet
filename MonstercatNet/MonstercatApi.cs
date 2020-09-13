using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    // https://github.com/defvs/connect-v2-docs/wiki/Log-in-out-and-session
    // TODO auto login-> TODO retrieve cookie expiration
    public sealed class MonstercatApi : IMonstercatApi
    {
        // v2.0.0 as first release
        // ignore schema hanges, if they happen and just release a new version
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
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials)
        {
            return _service.Login(credentials);
        }

        public Task Logout()
        {
            return _service.Logout();
        }

        public Task Login(string twoFactorAuthToken)
        {
            return _service.Login(twoFactorAuthToken);
        }

        public Task Resend(string twoFactorAuthToken)
        {
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
            return _service.SearchTracks(request);
        }

        public Task<ReleaseBrowseResult> GetReleases(ReleaseBrowseRequest request)
        {
            return _service.GetReleases(request);
        }

        public Task<ReleaseResult> GetRelease(string catalogId)
        {
            return _service.GetRelease(catalogId);
        }

        public Task<HttpContent> GetReleaseCover(ReleaseCoverRequest request)
        {
            return _service.GetReleaseCover(request);
        }

        public Task<HttpContent> DownloadRelease(ReleaseDownloadRequest request)
        {
            return _service.DownloadRelease(request);
        }
    }
}
