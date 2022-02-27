using System.IO;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public static class MonstercatApiExtensions
    {
        /// <summary>
        /// gold membership required
        /// </summary>
        public static async Task<byte[]> DownloadTrackAsByteArray(this IMonstercatApi api, TrackDownloadRequest request)
        {
            var content = await api.DownloadTrack(request).ConfigureAwait(false);

            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// gold membership required
        /// </summary>
        public static async Task<Stream> DownloadTrackAsStream(this IMonstercatApi api, TrackDownloadRequest request)
        {
            var content = await api.DownloadTrack(request).ConfigureAwait(false);

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public static async Task<Stream> StreamTrackAsStream(this IMonstercatApi api, TrackStreamRequest request)
        {
            var content = await api.StreamTrack(request).ConfigureAwait(false);

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }
    }
}
