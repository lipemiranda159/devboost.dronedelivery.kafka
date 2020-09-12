using devboost.dronedelivery.domain.Extensions;
using Xunit;

namespace devboost.dronedelivery.test.DTO.Extensions
{
    public class PedidoExtensionsTests
    {
        [Fact]
        public void GetPointTest()
        {
            var pedido = SetupTests.GetPedido();
            var point = pedido.GetPoint();
            Assert.True(pedido.Cliente.Longitude == point.Longitude && pedido.Cliente.Latitude == point.Latitude);
        }
    }
}
