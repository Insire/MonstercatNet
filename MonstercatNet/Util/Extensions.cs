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

        public static async Task<byte[]> DownloadReleaseAsByteArray(this IMonstercatApi api, ReleaseDownloadRequest request)
        {
            var content = await api.DownloadRelease(request).ConfigureAwait(false);

            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public static async Task<Stream> DownloadReleaseAsStream(this IMonstercatApi api, ReleaseDownloadRequest request)
        {
            var content = await api.DownloadRelease(request).ConfigureAwait(false);

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public static async Task<byte[]> DownloadTrackAsByteArray(this IMonstercatApi api, TrackDownloadRequest request)
        {
            var content = await api.DownloadTrack(request).ConfigureAwait(false);

            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public static async Task<Stream> DownloadTrackAsStream(this IMonstercatApi api, TrackDownloadRequest request)
        {
            var content = await api.DownloadTrack(request).ConfigureAwait(false);

            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }
    }
}
