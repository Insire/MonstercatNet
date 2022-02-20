using System;
using System.Web;

namespace SoftThorn.MonstercatNet
{
    public static class ReleaseCoverUrlBuilderExtension
    {
        public static ReleaseCoverArtBuilder ForRelease(this ReleaseCoverArtBuilder builder, TrackRelease release)
        {
            if (release is null)
            {
                throw new ArgumentNullException(nameof(release));
            }

            builder.Release = release;

            return builder;
        }

        /// <summary>
        /// Configure a webp image with a width of 300
        /// </summary>
        public static ReleaseCoverArtBuilder WithSmallCoverArt(this ReleaseCoverArtBuilder builder)
        {
            builder.Size = ContentSize.Width_300;
            builder.Encoding = ContentEncoding.WebP;

            return builder;
        }

        /// <summary>
        /// Configure a webp image with a width of 600
        /// </summary>
        public static ReleaseCoverArtBuilder WithMediumCoverArt(this ReleaseCoverArtBuilder builder)
        {
            builder.Size = ContentSize.Width_600;
            builder.Encoding = ContentEncoding.WebP;

            return builder;
        }

        /// <summary>
        /// Configure a webp image with a width of 1024
        /// </summary>
        public static ReleaseCoverArtBuilder WithLargeCoverArt(this ReleaseCoverArtBuilder builder)
        {
            builder.Size = ContentSize.Width_1024;
            builder.Encoding = ContentEncoding.WebP;

            return builder;
        }

        /// <summary>
        /// Configure a jpeg image with a width of 3000
        /// </summary>
        public static ReleaseCoverArtBuilder WithHugeCoverArt(this ReleaseCoverArtBuilder builder)
        {
            builder.Size = ContentSize.Width_3000;
            builder.Encoding = ContentEncoding.JPEG;

            return builder;
        }

        /// <summary>
        /// Create an uri to an webp image with a width of 300
        /// </summary>
        public static Uri GetSmallCoverArtUri(this TrackRelease release)
        {
            return GetCoverArtUri(release, 300, "webp");
        }

        /// <summary>
        /// Create an uri to an webp image with a width of 600
        /// </summary>
        public static Uri GetMediumCoverArtUri(this TrackRelease release)
        {
            return GetCoverArtUri(release, 600, "webp");
        }

        /// <summary>
        /// Create an uri to an webp image with a width of 1024
        /// </summary>
        public static Uri GetLargeCoverArtUri(this TrackRelease release)
        {
            return GetCoverArtUri(release, 1024, "webp");
        }

        /// <summary>
        /// Create an uri to an jpeg image with a width of 3000
        /// </summary>
        public static Uri GetHugeCoverArtUri(this TrackRelease release)
        {
            return GetCoverArtUri(release, 3000, "jpeg");
        }

        private static Uri GetCoverArtUri(this TrackRelease release, int size, string encoding)
        {
            var encodedUrl = HttpUtility.UrlEncode(CreateCoverArtUri(release));

            return new Uri($"{MonstercatEndpoints.CDN}/?width={size}&encoding={encoding}&url={encodedUrl}", UriKind.RelativeOrAbsolute);
        }

        public static string CreateCoverArtUri(this TrackRelease release)
        {
            return $"{MonstercatEndpoints.BASE}/release/{release.CatalogId}/cover";
        }
    }
}
