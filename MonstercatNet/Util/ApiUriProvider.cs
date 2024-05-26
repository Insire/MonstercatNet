using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class ApiUriProvider : IUriProvider
    {
        public static IUriProvider Instance { get; } = new ApiUriProvider();

        public Uri GetUri()
        {
            return new Uri(MonstercatEndpoints.API);
        }
    }
}
