using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public static class MonstercatApiExtensions
    {
        /// <summary>
        /// gold membership required
        /// </summary>
        public static async Task<byte[]> DownloadTrackAsByteArray(this IMonstercatApi api, TrackDownloadRequest request, CancellationToken token = default)
        {
            var content = await api.DownloadTrack(request, token).ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// gold membership required
        /// </summary>
        public static async Task<Stream> DownloadTrackAsStream(this IMonstercatApi api, TrackDownloadRequest request, CancellationToken token = default)
        {
            var content = await api.DownloadTrack(request, token).ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public static async Task<Stream> StreamTrackAsStream(this IMonstercatApi api, TrackStreamRequest request, CancellationToken token = default)
        {
            var content = await api.StreamTrack(request, token).ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }
    }
}
