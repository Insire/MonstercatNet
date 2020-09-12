using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    // TODO auto login-> TODO retrieve cookie expiration
    public sealed class MonstercatApi : IMonstercatApi
    {
        private const string BaseUrl = "https://connect.monstercat.com/v2/";

        /// <summary>
        /// Generate the client to be able to interact with the monstercat api. Comes with logging to Debug builtin
        /// </summary>
        /// <returns></returns>
        public static IMonstercatApi Create()
        {
            return Create(new HttpClient(new HttpLoggingHandler()));
        }

        /// <summary>
        /// Generate the client to be able to interact with the monstercat api
        /// </summary>
        /// <param name="client">the httpclient to use for all requests</param>
        /// <returns></returns>
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

            return new MonstercatApi(RestService.For<IMonstercatApi>(client), client);
        }

        private readonly IMonstercatApi _service;
        private readonly HttpClient _httpClient;

        public bool IsLoggedIn { get; private set; }

        private MonstercatApi(IMonstercatApi service, HttpClient httpClient)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task Login([Body(BodySerializationMethod.Serialized)] ApiCredentials credentials)
        {
            try
            {
                await _service.Login(credentials).ConfigureAwait(false);
                IsLoggedIn = true; // quite the optimistic approach. needs to be expanded to actually checking cookie in the httpclient
            }
            catch
            {
                IsLoggedIn = false;
                throw;
            }
        }

        public async Task Logout()
        {
            try
            {
                await _service.Logout().ConfigureAwait(false);
                IsLoggedIn = false; // quite the optimistic approach. needs to be expanded to actually checking cookie in the httpclient
            }
            catch
            {
                IsLoggedIn = true;
                throw;
            }
        }

        public Task Login2FA(string token)
        {
            return _service.Login2FA(token);
        }

        public Task Resend2FA(string token)
        {
            return _service.Resend2FA(token);
        }

        public Task<Self> GetSelf()
        {
            return _service.GetSelf();
        }

        public Task<PlaylistBrowseResult> GetSelfPlaylists()
        {
            return _service.GetSelfPlaylists();
        }

        public Task<ReleaseBrowseResult> GetReleases()
        {
            return _service.GetReleases();
        }
    }
}
