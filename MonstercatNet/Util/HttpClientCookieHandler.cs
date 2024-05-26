using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public sealed class HttpClientCookieHandler : HttpClientHandler
    {
        private readonly ICookieProcessor _cookieProcessor;
        private readonly Uri _uri;

        public HttpClientCookieHandler(ICookieProcessor cookieProcessor, IUriProvider uriProvider)
        {
            CookieContainer = new CookieContainer();
            _cookieProcessor = cookieProcessor;
            _uri = uriProvider.GetUri();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await _cookieProcessor.PreProcessCookies(CookieContainer.GetCookies(_uri));

            var response = await base.SendAsync(request, cancellationToken);

            await _cookieProcessor.PostProcessCookies(response, CookieContainer.GetCookies(_uri));

            return response;
        }
    }
}
