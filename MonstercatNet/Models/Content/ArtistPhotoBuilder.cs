using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class ArtistPhotoBuilder : AbstractBuilder<ContentRequest>
    {
        /// <summary>
        /// Create a new builder configured with a small sized webp cover art
        /// </summary>
        public static ArtistPhotoBuilder Create()
        {
            return Create(null);
        }

        /// <summary>
        /// Create a new builder configured with a small sized webp cover art
        /// </summary>
        public static ArtistPhotoBuilder Create(Artist? artist)
        {
            return new ArtistPhotoBuilder()
            {
                Encoding = ContentEncoding.WebP,
                Size = ContentSize.Width_256,
                Artist = artist,
            };
        }

        public ContentEncoding? Encoding { get; set; }
        public ContentSize? Size { get; set; }
        public Artist? Artist { get; set; }

        private ArtistPhotoBuilder()
        {
        }

        public override ContentRequest Build()
        {
            if (Artist is null)
            {
                throw new ArgumentNullException(nameof(Artist));
            }

            if (Artist.Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Artist.Id));
            }

            if (string.IsNullOrEmpty(Artist.Name))
            {
                throw new ArgumentNullException(nameof(Artist.Name));
            }

            var uri = Artist.CreateArtistPhotoUri();
            int size;
            string? encoding;

            switch (Encoding)
            {
                case ContentEncoding.WebP:
                    encoding = "webp";
                    size = Size switch
                    {
                        ContentSize.Width_256 => 256,
                        ContentSize.Width_300
                        or ContentSize.Width_600 => throw new ArgumentOutOfRangeException(nameof(Size)),
                        ContentSize.Width_1024 => 1024,
                        ContentSize.Width_1920
                        or ContentSize.Width_3000 => throw new ArgumentOutOfRangeException(nameof(Size)),
                        _ => throw new NotImplementedException(nameof(Size)),
                    };
                    break;

                case ContentEncoding.JPEG:
                    encoding = "jpeg";
                    size = Size switch
                    {
                        ContentSize.Width_256
                        or ContentSize.Width_300
                        or ContentSize.Width_600
                        or ContentSize.Width_1024
                        or ContentSize.Width_1920 => throw new ArgumentOutOfRangeException(nameof(Size)),
                        ContentSize.Width_3000 => 3000,
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
