using System.IO;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public static class MontercatCdnExtensions
    {
        public static async Task<byte[]> GetReleaseCoverAsByteArray(this IMonstercatCdnService api, ReleaseCoverArtBuilder builder)
        {
            var content = await api.GetReleaseCoverArt(builder).ConfigureAwait(false);

            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public static async Task<Stream> GetReleaseCoverAsStream(this IMonstercatCdnService api, ReleaseCoverArtBuilder builder)
        {
            var content = await api.GetReleaseCoverArt(builder).ConfigureAwait(false);

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }
    }
}
