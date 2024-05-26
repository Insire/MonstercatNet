using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet
{
    public interface ICookieProcessor
    {
        Task PreProcessCookies(CookieCollection cookies);

        Task PostProcessCookies(HttpResponseMessage message, CookieCollection cookies);
    }
}
