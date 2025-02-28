using System.Net;

namespace SoftThorn.MonstercatNet
{
    public sealed class NullCookieProcessor : ICookieProcessor
    {
        public static ICookieProcessor Instance { get; } = new NullCookieProcessor();

        public Task PostProcessCookies(HttpResponseMessage message, CookieCollection cookies)
        {
            return Task.CompletedTask;
        }

        public Task PreProcessCookies(CookieCollection cookies)
        {
            return Task.CompletedTask;
        }
    }
}
