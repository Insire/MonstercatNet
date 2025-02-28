namespace SoftThorn.MonstercatNet
{
    public interface IMonstercatCdnService
    {
        Task<HttpContent> GetReleaseCoverArt(ReleaseCoverArtBuilder builder, CancellationToken token = default);

        Task<HttpContent> GetArtistPhoto(ArtistPhotoBuilder builder, CancellationToken token = default);
    }
}
