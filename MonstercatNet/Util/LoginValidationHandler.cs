using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public sealed class LoginValidationHandler : DelegatingHandler
    {
        private HttpResponseHeaders? _headers;

        public LoginValidationHandler()
            : base()
        {
        }

        public LoginValidationHandler(HttpMessageHandler httpMessageHandler)
            : base(httpMessageHandler)
        {
        }

        public bool HasMonstercatLogin()
        {
            var headers = _headers;

            return headers?.Contains("Set-Cookie") == true;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            _headers = response.Headers;

            return response;
        }
    }
}
