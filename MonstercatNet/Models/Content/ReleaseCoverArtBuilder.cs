using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class ReleaseCoverArtBuilder : AbstractBuilder<ContentRequest>
    {
        /// <summary>
        /// Create a new builder configured with a medium sized webp cover art
        /// </summary>
        public static ReleaseCoverArtBuilder Create()
        {
            return Create(null);
        }

        /// <summary>
        /// Create a new builder configured with a medium sized webp cover art
        /// </summary>
        public static ReleaseCoverArtBuilder Create(TrackRelease? release)
        {
            return new ReleaseCoverArtBuilder()
            {
                Encoding = ContentEncoding.WebP,
                Size = ContentSize.Width_600,
                Release = release,
            };
        }

        public ContentEncoding? Encoding { get; set; }
        public ContentSize? Size { get; set; }
        public TrackRelease? Release { get; set; }

        private ReleaseCoverArtBuilder()
        {
        }

        public override ContentRequest Build()
        {
            if (Release is null)
            {
                throw new ArgumentNullException(nameof(Release));
            }

            if (string.IsNullOrWhiteSpace(Release.CatalogId))
            {
                throw new ArgumentNullException(nameof(Release.CatalogId));
            }

            var uri = Release.CreateCoverArtUri();
            int size;
            string? encoding;

            switch (Encoding)
            {
                case ContentEncoding.WebP:
                    encoding = "webp";
                    size = Size switch
                    {
                        ContentSize.Width_300 => 300,
                        ContentSize.Width_600 => 600,
                        ContentSize.Width_1024 => 1024,
                        ContentSize.Width_1920 => 1920,
                        ContentSize.Width_3000 => throw new ArgumentOutOfRangeException(nameof(Size)),
                        _ => throw new NotImplementedException(nameof(Size)),
                    };
                    break;

                case ContentEncoding.JPEG:
                    encoding = "jpeg";
                    size = Size switch
                    {
                        ContentSize.Width_3000 => 3000,
                        ContentSize.Width_300
                        or ContentSize.Width_600
                        or ContentSize.Width_1024
                        or ContentSize.Width_1920 => throw new ArgumentOutOfRangeException(nameof(Size)),
                        _ => throw new NotImplementedException(nameof(Size)),
                    };
                    break;

                default:
                    throw new NotImplementedException(nameof(Encoding));
            }

            return new ContentRequest()
            {
                Encoding = encoding,
                Width = size,
                Url = uri,
            };
        }
    }
}
