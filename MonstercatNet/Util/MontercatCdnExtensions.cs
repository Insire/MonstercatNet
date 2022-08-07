using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public static class MontercatCdnExtensions
    {
        public static async Task<byte[]> GetReleaseCoverAsByteArray(this IMonstercatCdnService api, ReleaseCoverArtBuilder builder, CancellationToken token = default)
        {
            var content = await api.GetReleaseCoverArt(builder, token).ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public static async Task<Stream> GetReleaseCoverAsStream(this IMonstercatCdnService api, ReleaseCoverArtBuilder builder, CancellationToken token = default)
        {
            var content = await api.GetReleaseCoverArt(builder, token).ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public static async Task<byte[]> GetArtistPhotoAsByteArray(this IMonstercatCdnService api, ArtistPhotoBuilder builder, CancellationToken token = default)
        {
            var content = await api.GetArtistPhoto(builder, token).ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public static async Task<Stream> GetArtistPhotoAsStream(this IMonstercatCdnService api, ArtistPhotoBuilder builder, CancellationToken token = default)
        {
            var content = await api.GetArtistPhoto(builder, token).ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }
    }
}
