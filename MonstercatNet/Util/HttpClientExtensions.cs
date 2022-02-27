using System;
using System.Net.Http;

namespace SoftThorn.MonstercatNet
{
    public static class HttpClientExtensions
    {
        private static readonly Uri _cdnUri = new Uri(MonstercatEndpoints.CDN);
        private static readonly Uri _apiUri = new Uri(MonstercatEndpoints.API);

        public static HttpClient UseMonstercatApiV2(this HttpClient client)
        {
            client.BaseAddress = _apiUri;

            return client;
        }

        public static HttpClient UseMonstercatCdn(this HttpClient client)
        {
            client.BaseAddress = _cdnUri;

            return client;
        }
    }
}
