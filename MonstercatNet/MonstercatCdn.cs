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
        /// <param name="client">the httpclient to use for all requests</param>
        public static IMonstercatCdnService Create(HttpClient client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return new MonstercatCdn(RestService.For<IMonstercatCdn>(client, Settings));
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
    }
}
