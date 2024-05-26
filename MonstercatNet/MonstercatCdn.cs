using Refit;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public sealed class MonstercatCdn : MonstercatBase, IMonstercatCdnService
    {
        /// <summary>
        /// Generate the client to be able to interact with the monstercat CDN
        /// </summary>
        /// <param name="httpClient">the httpclient to use for all requests</param>
        public static IMonstercatCdnService Create(HttpClient httpClient)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            return new MonstercatCdn(RestService.For<IMonstercatCdn>(httpClient, Settings));
        }

        public static IMonstercatCdnService Create()
        {
            var httpClient = new HttpClient(new HttpClientCookieHandler(NullCookieProcessor.Instance, CdnUriProvider.Instance)).UseMonstercatCdn();

            return new MonstercatCdn(RestService.For<IMonstercatCdn>(httpClient, Settings));
        }

        private readonly IMonstercatCdn _monsercatCdn;

        private MonstercatCdn(IMonstercatCdn monsercatCdn)
        {
            _monsercatCdn = monsercatCdn;
        }

        public Task<HttpContent> GetReleaseCoverArt(ReleaseCoverArtBuilder builder, CancellationToken token = default)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return _monsercatCdn.GetContent(builder.Build(), token);
        }

        public Task<HttpContent> GetArtistPhoto(ArtistPhotoBuilder builder, CancellationToken token = default)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return _monsercatCdn.GetContent(builder.Build(), token);
        }
    }
}
