#nullable disable

namespace SoftThorn.MonstercatNet.Tests.Util
{
    public sealed class ApiTestFixture : IDisposable
    {
        internal IMonstercatCdnService Cdn { get; private set; }

        internal IMonstercatApi Api { get; private set; }

        internal ApiCredentials Credentials { get; } = new ApiCredentials();

        internal LoginValidationHandler LoginValidationHandler { get; } = new LoginValidationHandler(HttpLoggingHandler.Create());

        public bool IsLoggedIn => LoginValidationHandler.HasMonstercatLogin();

        public ApiTestFixture()
        {
            var configuration = new ConfigurationBuilder()
#if DEBUG
                .AddUserSecrets<ApiTestFixture>()
#endif
                .AddEnvironmentVariables()
                .Build();

            var sectionName = nameof(ApiCredentials);
            var section = configuration.GetSection(sectionName);

            section.Bind(Credentials);

            // For tests to be able to run, you need to provide a monstercat account via the usersecrets of the MonstercatNet.Tests project or environment variables.
            Assert.NotNull(Credentials.Email);
            Assert.NotNull(Credentials.Password);

            Api = MonstercatApi.Create(new HttpClient(LoginValidationHandler).UseMonstercatApiV2());
            Cdn = MonstercatCdn.Create(new HttpClient(HttpLoggingHandler.Create()).UseMonstercatCdn());
        }

        public void Dispose()
        {
            LoginValidationHandler.Dispose();
        }
    }
}
