using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Entities;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.Infra.Data;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Repositories
{
    public class DroneRepositoryTests
    {
        private readonly DataContext _context;
        private readonly DroneRepository _droneRepository;
        private readonly ICommandExecutor<DroneStatusResult> _droneStatusExecutor;
        private readonly ICommandExecutor<StatusDroneDto> _statusDroneExecutor;


        public DroneRepositoryTests()
        {
            _context = ContextProvider<Drone>.GetContext(SetupTests.GetDrones(1));
            _droneStatusExecutor = new CommandExecutorTest<DroneStatusResult>();
            _statusDroneExecutor = Substitute.For<ICommandExecutor<StatusDroneDto>>();
            _droneRepository = new DroneRepository(_context, _statusDroneExecutor, _droneStatusExecutor);
        }

        [Fact]
        public async Task GetDroneTests()
        {
            var drone = await _droneRepository.GetByIdAsync(1);
            Assert.True(drone.Equals(SetupTests.GetDrone(1)));
        }
        [Fact]
        public void GetDroneStatusTest()
        {
            _statusDroneExecutor.ExcecuteCommand(Arg.Any<string>())
                .Returns(SetupTests.GetListStatusDroneDto());
            var droneStatus = _droneRepository.GetDroneStatus();
            Assert.True(droneStatus.Any());
        }

        [Fact]
        public async Task SaveDroneTest()
        {
            var drone = await _droneRepository.GetByIdAsync(1);
            drone.Velocidade += 10;
            drone.Autonomia += 80;
            drone.SetPerformance();
            var droneResult = await _droneRepository.UpdateAsync(drone);
            Assert.True(droneResult.Perfomance == 330);
        }

        [Fact]
        public void RetornaDroneTest()
        {
            var drone = _droneRepository.RetornaDrone();

            Assert.True(drone != null);
        }

        [Fact]
        public void RetornaDroneStatusTest()
        {
            var status = _droneRepository.RetornaDroneStatus(1);

            Assert.True(status != null);

        }
    }
}