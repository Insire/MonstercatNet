#nullable disable

using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net.Http;

namespace SoftThorn.MonstercatNet.Tests
{
    public abstract class ApiTestBase
    {
        protected internal IMonstercatCdnService Cdn { get; private set; }
        protected internal IMonstercatApi Api { get; private set; }
        protected internal ApiCredentials Credentials { get; } = new ApiCredentials();
        protected internal LoginValidationHandler LoginValidationHandler { get; } = new LoginValidationHandler(new HttpLoggingHandler());

        protected bool IsLoggedIn => LoginValidationHandler.HasMonstercatLogin();

        [OneTimeSetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ApiTestBase>()
                .AddEnvironmentVariables()
                .Build();

            var sectionName = typeof(ApiCredentials).Name;
            var section = configuration.GetSection(sectionName);

            section.Bind(Credentials);

            Credentials.Email.Should().NotBeNullOrWhiteSpace("For tests to be able to run, you need to provide a monstercat account via the usersecrets of the MonstercatNet.Tests project.");
            Credentials.Password.Should().NotBeNullOrWhiteSpace("For tests to be able to run, you need to provide the account password.");

            Api = MonstercatApi.Create(new HttpClient(LoginValidationHandler).UseMonstercatApiV2());
            Cdn = MonstercatCdn.Create(new HttpClient(new HttpLoggingHandler()).UseMonstercatCdn());
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            LoginValidationHandler.Dispose();
        }
    }
}
