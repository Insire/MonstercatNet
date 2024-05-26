using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class CdnUriProvider : IUriProvider
    {
        public static IUriProvider Instance { get; } = new CdnUriProvider();

        public Uri GetUri()
        {
            return new Uri(MonstercatEndpoints.CDN);
        }
    }
}
