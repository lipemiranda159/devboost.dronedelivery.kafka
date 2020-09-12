using devboost.dronedelivery.core.domain;
using Xunit;

namespace devboost.dronedelivery.test.DTO
{
    public class DroneStatusDTOTests
    {
        [Fact]
        public void DroneStatusDTOConstructorTest()
        {
            var drone = SetupTests.GetDrone();
            var droneStatus = new DroneStatusDto(drone);
            Assert.True(droneStatus.Drone.Autonomia == drone.Autonomia);
        }
    }
}
