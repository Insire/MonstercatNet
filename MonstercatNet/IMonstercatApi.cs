using Refit;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public interface IMonstercatApi
    {
        [Get("/self")]
        Task<Self> GetSelf();
    }
}
