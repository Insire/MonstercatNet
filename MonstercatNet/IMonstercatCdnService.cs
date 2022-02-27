using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public interface IMonstercatCdnService
    {
        Task<HttpContent> GetReleaseCoverArt(ReleaseCoverArtBuilder builder, CancellationToken token = default);
    }
}
