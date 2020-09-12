using devboost.dronedelivery.core.domain;
using Xunit;

namespace devboost.dronedelivery.test.DTO
{
    public class PointTests
    {
        private const double LATITUDE_BASE = -23.5880684;
        private const double LONGITUDE_BASE = -46.6564195;

        [Fact]
        public void TestPointConstructorPassingCoord()
        {
            var pedido = SetupTests.GetPedido();
            var point = new Point(pedido.Cliente.Latitude, pedido.Cliente.Longitude);
            Assert.True(point.Latitude == pedido.Cliente.Latitude && point.Longitude == pedido.Cliente.Longitude);
        }

        [Fact]
        public void TestPointConstructor()
        {
            var point = new Point();
            Assert.True(point.Latitude == LATITUDE_BASE && point.Longitude == LONGITUDE_BASE);

        }
    }
}
