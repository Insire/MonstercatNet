using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace SoftThorn.MonstercatNet.Tests;

[Category(Categories.IntegrationTest)]
[Category(Categories.InMemory)]
public sealed class PollyPolicyTests
{
    private static async Task PrepareAndRunTest(int totalRetries, FakeHttpDelegatingHandler handler, Func<HttpClient, Task> testInvcation)
    {
        var services = new ServiceCollection();
        services
            .AddHttpClient("my-httpclient", httpclient => httpclient.BaseAddress = new Uri("https://localhost"))
            .AddPolicyHandler(HttpClientPolicies.DefaultRetryPolicy(totalRetries, sleepDurationProvider: _ => TimeSpan.FromMilliseconds(1)))
            .AddHttpMessageHandler(() => handler);

        await using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        using var sut = scope.ServiceProvider
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient("my-httpclient");

        await testInvcation.Invoke(sut);
    }

    [Fact]
    public Task ShouldRetryFailingRequestsThreeTimes()
    {
        const int totalRequests = 4;
        const int totalRetries = 3;

        var fakeHttpDelegatingHandler = new FakeHttpDelegatingHandler(_ => Task.FromResult(new HttpResponseMessage(HttpStatusCode.GatewayTimeout)));

        return PrepareAndRunTest(totalRetries, fakeHttpDelegatingHandler, async sut =>
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/any");

            var result = await sut.SendAsync(request);

            Assert.Equal(HttpStatusCode.GatewayTimeout, result.StatusCode);
            Assert.Equal(totalRequests, fakeHttpDelegatingHandler.Requests);
        });
    }

    [Fact]
    public Task ShouldStopRetryingIfSuccessStatusCodeIsEncountered()
    {
        const int totalRequests = 2;
        const int totalRetries = 3;

        var fakeHttpDelegatingHandler = new FakeHttpDelegatingHandler(attempt =>
        {
            return attempt switch
            {
                2 => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)),
                _ => Task.FromResult(new HttpResponseMessage(HttpStatusCode.GatewayTimeout))
            };
        });

        return PrepareAndRunTest(totalRetries, fakeHttpDelegatingHandler, async sut =>
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/any");

            var result = await sut.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(totalRequests, fakeHttpDelegatingHandler.Requests);
        });
    }
}
