namespace SoftThorn.MonstercatNet.Tests;

[Category(Categories.UnitTest)]
[Category(Categories.InMemory)]
public sealed class RequestBaseValidationTests
{
    [Fact]
    public void Fact_LimitCantExceedMaxLimit()
    {
        var request = new FactRequest();
        Assert.Equal(RequestBase.MaxLimit, request.Limit);

        request.Limit = RequestBase.MaxLimit + 1;
        Assert.Equal(RequestBase.MaxLimit, request.Limit);
    }

    [Fact]
    public void Fact_LimitCantBeLowerThanMinLimit()
    {
        var request = new FactRequest();
        Assert.Equal(RequestBase.MaxLimit, request.Limit);

        request.Limit = RequestBase.MinLimit - 1;
        Assert.Equal(RequestBase.MinLimit, request.Limit);
    }

    [Fact]
    public void Fact_SkipCantBeLowerThanMinSkip()
    {
        var request = new FactRequest();
        Assert.Equal(RequestBase.MinSkip, request.Skip);

        request.Skip = RequestBase.MinSkip - 1;
        Assert.Equal(RequestBase.MinSkip, request.Skip);
    }

    private sealed class FactRequest : RequestBase;
}
