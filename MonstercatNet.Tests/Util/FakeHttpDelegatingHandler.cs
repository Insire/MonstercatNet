namespace SoftThorn.MonstercatNet.Tests;

public sealed class FakeHttpDelegatingHandler : DelegatingHandler
{
    private readonly Func<int, Task<HttpResponseMessage>> _responseFactory;

    public int Requests { get; private set; }

    public FakeHttpDelegatingHandler(Func<int, Task<HttpResponseMessage>> responseFactory)
    {
        _responseFactory = responseFactory ?? throw new ArgumentNullException(nameof(responseFactory));
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await _responseFactory.Invoke(++Requests);
    }
}
