using Polly;
using Polly.Extensions.Http;

namespace SoftThorn.MonstercatNet;

public static class HttpClientPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> DefaultRetryPolicy(
        int retryAttempts = 3,
        Func<int, TimeSpan>? sleepDurationProvider = null)
    {
        sleepDurationProvider ??= (retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(retryAttempts, sleepDurationProvider);
    }
}
