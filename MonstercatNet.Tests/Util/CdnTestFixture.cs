#nullable disable

namespace SoftThorn.MonstercatNet.Tests.Util
{
    public sealed class CdnTestFixture
    {
        private static IMonstercatCdnService Create()
        {
            return MonstercatCdn.Create(new HttpClient(new HttpLoggingHandler()).UseMonstercatCdn());
        }

        internal IMonstercatCdnService Cdn { get; private set; }

        public CdnTestFixture()
        {
            Cdn = Create();
        }
    }
}
