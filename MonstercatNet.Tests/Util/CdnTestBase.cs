#nullable disable

using NUnit.Framework;
using System.Net.Http;

namespace SoftThorn.MonstercatNet.Tests
{
    public abstract class CdnTestBase
    {
        private static IMonstercatCdnService Create()
        {
            return MonstercatCdn.Create(new HttpClient(new HttpLoggingHandler()).UseMonstercatCdn());
        }

        protected internal IMonstercatCdnService Cdn { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            Cdn = Create();
        }
    }
}
