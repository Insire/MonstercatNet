using Refit;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public interface IMonstercatCdn
    {
        /// <summary>
        /// no login required
        /// </summary>
        [Get("/")]
        Task<HttpContent> GetContent([Query] ContentRequest request, CancellationToken token = default);
    }
}
