using System.Net;

namespace SoftThorn.MonstercatNet
{
    public interface ICookieProcessor
    {
        Task PreProcessCookies(CookieCollection cookies);

        Task PostProcessCookies(HttpResponseMessage message, CookieCollection cookies);
    }
}
