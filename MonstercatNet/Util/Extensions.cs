using System.IO;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public static class Extensions
    {
        public static async Task<byte[]> GetReleaseCoverAsByteArray(this IMonstercatApi api, ReleaseCoverRequest request)
        {
            var content = await api.GetReleaseCover(request).ConfigureAwait(false);

            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public static async Task<Stream> GetReleaseCoverAsStream(this IMonstercatApi api, ReleaseCoverRequest request)
        {
            var content = await api.GetReleaseCover(request).ConfigureAwait(false);

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }
    }
}
