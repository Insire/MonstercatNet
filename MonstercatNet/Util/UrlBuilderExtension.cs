using System;
using System.Net;

namespace SoftThorn.MonstercatNet
{
    public static class UrlBuilderExtension
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
        /// Configure a webp image with a width of 256
        /// </summary>
        public static ArtistPhotoBuilder WithSmallPhoto(this ArtistPhotoBuilder builder)
        {
            builder.Size = ContentSize.Width_256;
            builder.Encoding = ContentEncoding.WebP;

            return builder;
        }

        /// <summary>
        /// Configure a webp image with a width of 1024
        /// </summary>
        public static ArtistPhotoBuilder WithLargePhoto(this ArtistPhotoBuilder builder)
        {
            builder.Size = ContentSize.Width_1024;
            builder.Encoding = ContentEncoding.WebP;

            return builder;
        }

        /// <summary>
        /// Configure a jpeg image with a width of 3000
        /// </summary>
        public static ArtistPhotoBuilder WithHugePhoto(this ArtistPhotoBuilder builder)
        {
            builder.Size = ContentSize.Width_3000;
            builder.Encoding = ContentEncoding.JPEG;

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
            var encodedUrl = WebUtility.UrlEncode(CreateCoverArtUri(release));

            return new Uri($"{MonstercatEndpoints.CDN}/?width={size}&encoding={encoding}&url={encodedUrl}", UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Create base uri for fetching release cover art
        /// </summary>
        public static string CreateCoverArtUri(this TrackRelease track)
        {
            if (track is null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            return CreateCoverArtUri(track.CatalogId);
        }

        /// <summary>
        /// Create base uri for fetching release cover art
        /// </summary>
        public static string CreateCoverArtUri(this Track track)
        {
            if (track is null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            if (track.Release is null)
            {
                throw new ArgumentNullException(nameof(track.Release));
            }

            return CreateCoverArtUri(track.Release.CatalogId);
        }

        private static string CreateCoverArtUri(string catalogId)
        {
            return $"{MonstercatEndpoints.BASE}/release/{catalogId}/cover";
        }

        /// <summary>
        /// Create an uri to an webp image with a width of 256
        /// </summary>
        public static Uri GetSmallArtistPhotoUri(this TrackArtist artist)
        {
            return GetArtistPhotoUri(artist, 256, "webp");
        }

        /// <summary>
        /// Create an uri to an webp image with a width of 256
        /// </summary>
        public static Uri GetSmallArtistPhotoUri(this Artist artist)
        {
            return GetArtistPhotoUri(artist, 256, "webp");
        }

        /// <summary>
        /// Create an uri to an webp image with a width of 1024
        /// </summary>
        public static Uri GetLargeArtistPhotoUri(this TrackArtist artist)
        {
            return GetArtistPhotoUri(artist, 1024, "webp");
        }

        /// <summary>
        /// Create an uri to an webp image with a width of 1024
        /// </summary>
        public static Uri GetLargeArtistPhotoUri(this Artist artist)
        {
            return GetArtistPhotoUri(artist, 1024, "webp");
        }

        private static Uri GetArtistPhotoUri(this TrackArtist artist, int size, string encoding)
        {
            var encodedUrl = WebUtility.UrlEncode(CreateArtistPhotoUri(artist));

            return new Uri($"{MonstercatEndpoints.CDN}/?encoding={encoding}&url={encodedUrl}&width={size}", UriKind.RelativeOrAbsolute);
        }

        private static Uri GetArtistPhotoUri(this Artist artist, int size, string encoding)
        {
            var encodedUrl = WebUtility.UrlEncode(CreateArtistPhotoUri(artist));

            return new Uri($"{MonstercatEndpoints.CDN}/?encoding={encoding}&url={encodedUrl}&width={size}", UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Create base uri for fetching artist photo
        /// </summary>
        public static string CreateArtistPhotoUri(this TrackArtist artist)
        {
            if (artist is null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            return CreateArtistPhotoUri(artist.Uri, artist.Id.ToString());
        }

        /// <summary>
        /// Create base uri for fetching artist photo
        /// </summary>
        public static string CreateArtistPhotoUri(this Artist artist)
        {
            if (artist is null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            return CreateArtistPhotoUri(artist.Uri, artist.ArtistId.ToString());
        }

        private static string CreateArtistPhotoUri(string name, string id)
        {
            return $"{MonstercatEndpoints.BASE}/artist/{name}/photo?{id}";
        }
    }
}
