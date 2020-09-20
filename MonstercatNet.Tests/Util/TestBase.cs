using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net.Http;

namespace SoftThorn.MonstercatNet.Tests
{
    public abstract class TestBase
    {
        private static IMonstercatApi Create()
        {
            return MonstercatApi.Create(new HttpClient(new HttpLoggingHandler()).UseMonstercatApiV2());
        }

        protected internal IMonstercatApi Api { get; private set; }
        protected internal ApiCredentials Credentials { get; } = new ApiCredentials();

        [OneTimeSetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<TestBase>(optional: true)
                .AddEnvironmentVariables()
                .Build();

            var sectionName = typeof(ApiCredentials).Name;
            var section = configuration.GetSection(sectionName);

            Assert.IsNotNull(section, "no credentials found");

            section.Bind(Credentials);

            Assert.IsNotNull(Credentials.Email, "For tests to be able to run, you need to provide a monstercat account via the usersecrets of the MonstercatNet.Tests project.");
            Assert.IsNotNull(Credentials.Password, "For tests to be able to run, you need to provide the account password.");

            Api = Create();
        }
    }
}
