using devboost.dronedelivery.security.domain.Extensions;
using Xunit;

namespace devboost.dronedelivery.test.DTO.Extensions
{
    public class ClienteExtensionsTests
    {
        [Fact]
        public void ClienteHasUserTest()
        {
            var cliente = SetupTests.GetLogin();
            Assert.True(cliente.HasClient());
        }
    }
}
