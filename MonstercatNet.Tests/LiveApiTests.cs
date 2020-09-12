using MonstercatNet.Tests;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SoftThorn.MonstercatNet.Tests
{
    public sealed class LiveApiTests : TestBase
    {
        [Test, Order(2)]
        public async Task TestGetSelf()
        {
            var self = await Api.GetSelf();

            Assert.IsNotNull(self);
            Assert.AreEqual(Credentials.Email, self.Email);
            Assert.IsTrue(self.HasGold, "The test account should have an active gold subscription, otherwise some tests are bound to fail.");
        }

        [Test, Order(1)]
        public async Task TestGetLogin()
        {
            await Api.Login(Credentials);
        }

        [Test, Order(999)]
        public async Task TestLogout()
        {
            await Api.Logout();
        }
    }
}
