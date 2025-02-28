#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

namespace SoftThorn.MonstercatNet.Tests;

[Category(Categories.UnitTest)]
[Category(Categories.InMemory)]
public sealed class LoginRequestValidationTests(ApiTestFixture fixture) : IClassFixture<ApiTestFixture>
{
    private readonly ApiTestFixture _fixture = fixture;

    [Fact]
    public async Task Fact_NullEmail()
    {
        var request = new ApiCredentials()
        {
            Email = null,
            Password = "something"
        };

        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.Login(request));
    }

    [Fact]
    public async Task Fact_EmptyEmail()
    {
        var request = new ApiCredentials()
        {
            Email = "",
            Password = "something"
        };

        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.Login(request));
    }

    [Fact]
    public async Task Fact_NullPassword()
    {
        var request = new ApiCredentials()
        {
            Email = "something",
            Password = null
        };

        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.Login(request));
    }

    [Fact]
    public async Task Fact_EmptyPassword()
    {
        var request = new ApiCredentials()
        {
            Email = "something",
            Password = null
        };

        await Assert.ThrowsAsync<ArgumentNullException>(() => _fixture.Api.Login(request));
    }
}
