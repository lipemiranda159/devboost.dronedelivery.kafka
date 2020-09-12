using devboost.dronedelivery.Api.Controllers;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Controller
{
    public class DroneControllerTests
    {
        private readonly IDroneFacade _droneFacade;

        public DroneControllerTests()
        {
            _droneFacade = Substitute.For<IDroneFacade>();
        }

        [Fact]
        public void TestGetStatusDrone()
        {
            _droneFacade.GetDroneStatus().Returns(SetupTests.GetDroneStatus());
            var droneController = new DronesController(_droneFacade);
            droneController.GetStatusDrone();
            _droneFacade.Received().GetDroneStatus();

        }

        [Fact]
        public async Task PostDrone()
        {
            var droneController = new DronesController(_droneFacade);
            await droneController.PostDrone(SetupTests.GetDrone());
            await _droneFacade.Received().SaveDroneAsync(Arg.Any<Drone>());
        }
    }
}
