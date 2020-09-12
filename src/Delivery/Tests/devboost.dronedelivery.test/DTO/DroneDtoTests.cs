using devboost.dronedelivery.domain.Entities;
using Xunit;

namespace devboost.dronedelivery.test.DTO
{
    public class DroneDtoTests
    {
        [Fact]
        public void DroneDtoConstructorTests()
        {
            var drone = SetupTests.GetDrone();
            var droneDto = new DroneDto(SetupTests.GetDroneStatusDto(), 10);
            Assert.True(
                droneDto.Distancia == 10 &&
                droneDto.DroneStatus.SomaDistancia == 5 &&
                droneDto.DroneStatus.SomaPeso == 10);
        }
    }
}
