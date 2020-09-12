using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Entities;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.felipe.Facade;
using devboost.dronedelivery.Infra.Data;
using devboost.dronedelivery.test.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace devboost.dronedelivery.test.BDD
{
    [Binding]
    public sealed class AdicionarDroneSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IDroneRepository _droneRepository;
        private readonly IDroneService _droneService;
        private readonly IDroneFacade _droneFacade;
        private readonly DataContext _context;
        private readonly ICommandExecutor<DroneStatusResult> _droneStatusExecutor;
        private readonly ICommandExecutor<StatusDroneDto> _statusDroneExecutor;


        public AdicionarDroneSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _context = ContextProvider<Drone>.GetContext(null);
            _droneStatusExecutor = Substitute.For<ICommandExecutor<DroneStatusResult>>();
            _statusDroneExecutor = Substitute.For<ICommandExecutor<StatusDroneDto>>();
            _droneRepository = new DroneRepository(_context, _statusDroneExecutor, _droneStatusExecutor);
            _droneService = null;
            _droneFacade = new DroneFacade(_droneService, _droneRepository);
        }

        [Given("A Autonomia e (.*)")]
        public void GivenTheFirstNumberIs(int autonomia)
        {
            _scenarioContext.Add("autonomia", autonomia);
        }

        [Given("A velocidade e (.*)")]
        public void GivenTheSecondNumberIs(int velocidade)
        {
            _scenarioContext.Add("velocidade", velocidade);
        }

        [When("Quando criamos o Drone")]
        public async Task CriamosDrone()
        {
            var drone = new Drone()
            {
                Autonomia = _scenarioContext.Get<int>("autonomia"),
                Velocidade = _scenarioContext.Get<int>("velocidade")
            };
            await _droneFacade.SaveDroneAsync(drone);

            _scenarioContext.Add("drone", drone);
        }

        [Then("A performance deste Drone deve ser (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            var droneResult = _scenarioContext.Get<Drone>("drone");

            Assert.True(droneResult.Perfomance == 160F);
        }
    }
}
