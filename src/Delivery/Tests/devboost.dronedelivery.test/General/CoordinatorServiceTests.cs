using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.domain.Extensions;
using devboost.dronedelivery.Services;
using Xunit;

namespace devboost.dronedelivery.test.General
{
    public class CoordinatorServiceTests
    {
        [Fact]
        public void GetKmDistanceTest()
        {
            var coordinatiorService = new CoordinateService();
            var pedido = SetupTests.GetPedido();
            var distance = coordinatiorService.GetKmDistance(pedido.GetPoint(), new Point());
            Assert.True(distance == 489.8);
        }
    }
}
