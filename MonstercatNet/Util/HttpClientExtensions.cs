using System;
using System.Net.Http;

namespace SoftThorn.MonstercatNet
{
    public static class HttpClientExtensions
    {
        private const string BaseUrl = "https://www.monstercat.com/api";

        private static readonly Uri _uri = new Uri(BaseUrl);

        public static HttpClient UseMonstercatApiV2(this HttpClient client)
        {
            client.BaseAddress = _uri;

            return client;
        }
    }
}
