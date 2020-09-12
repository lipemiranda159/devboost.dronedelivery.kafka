using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Enums;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.felipe.Facade;
using devboost.dronedelivery.Services;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test
{
    public class AssignDroneTests
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoService _pedidoService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDroneRepository _droneRepository;
        private readonly IDroneService _droneService;
        private readonly ICoordinateService _coordinateService;
        private readonly IPedidoDroneRepository _pedidoDroneRepository;
        private readonly IPagamentoServiceFactory _pagamentoServiceFactory;
        public AssignDroneTests()
        {
            _clienteRepository = Substitute.For<IClienteRepository>();
            _pedidoService = Substitute.For<IPedidoService>();
            _droneRepository = Substitute.For<IDroneRepository>();
            _droneService = Substitute.For<IDroneService>();
            _pedidoRepository = Substitute.For<IPedidoRepository>();
            _coordinateService = Substitute.For<ICoordinateService>();
            _pedidoDroneRepository = Substitute.For<IPedidoDroneRepository>();
            _pagamentoServiceFactory = Substitute.For<IPagamentoServiceFactory>();
        }

        [Fact]
        public async Task TestMethodsCalled()
        {

            var pedidoFacade = new PedidoFacade(
                _pedidoService,
                _clienteRepository,
                _pedidoRepository,
                _droneRepository,
                _pagamentoServiceFactory,
                _pedidoDroneRepository);

            var pedidos = SetupTests.GetPedidosList();
            _pedidoRepository.ObterPedidos(Arg.Any<int>())
                .Returns(pedidos);
            _clienteRepository.GetByIdAsync(Arg.Any<int>())
                .Returns(SetupTests.GetCliente());
            _pedidoService.DroneAtendePedido(pedidos[0])
                .Returns(SetupTests.GetDroneDto());
            _droneRepository.GetByIdAsync(Arg.Any<int>())
                .Returns(SetupTests.GetDrone());
            _pedidoRepository.GetByIdAsync(Arg.Any<int>())
                .Returns(pedidos[0]);

            await pedidoFacade.AssignDroneAsync();


            await _pedidoService.Received().DroneAtendePedido(Arg.Any<Pedido>());
            await _pedidoRepository.Received().UpdateAsync(Arg.Any<Pedido>());
            await _pedidoDroneRepository.Received().AddAsync(Arg.Any<PedidoDrone>());
        }
        [Fact]
        public async Task TestDroneNotFound()
        {

            var pedidoFacade = new PedidoFacade(
                _pedidoService,
                _clienteRepository,
                _pedidoRepository,
                _droneRepository,
                _pagamentoServiceFactory,
                _pedidoDroneRepository);

            var pedidos = SetupTests.GetPedidosList();
            _pedidoRepository.ObterPedidos(Arg.Any<int>())
                .Returns(pedidos);
            _clienteRepository.GetByIdAsync(Arg.Any<int>())
                .Returns(SetupTests.GetCliente());

            _droneRepository.GetByIdAsync(Arg.Any<int>())
                .Returns(SetupTests.GetDrone());
            _pedidoRepository.GetByIdAsync(Arg.Any<int>())
                .Returns(pedidos[0]);

            await pedidoFacade.AssignDroneAsync();


            await _pedidoRepository.DidNotReceive().UpdateAsync(Arg.Any<Pedido>());
        }
        [Fact]
        public async Task TestPedidoNotFound()
        {

            var pedidoFacade = new PedidoFacade(
                _pedidoService,
                _clienteRepository,
                _pedidoRepository,
                _droneRepository,
                _pagamentoServiceFactory,
                _pedidoDroneRepository);

            _clienteRepository.GetByIdAsync(Arg.Any<int>())
                .Returns(SetupTests.GetCliente());

            _droneRepository.GetByIdAsync(Arg.Any<int>())
                .Returns(SetupTests.GetDrone());

            await pedidoFacade.AssignDroneAsync();


            await _pedidoRepository.DidNotReceive().UpdateAsync(Arg.Any<Pedido>());
        }

        [Fact]
        public async Task TestDroneAtendePedidoQuandoDroneExiste()
        {
            var droneService = new DroneService(_coordinateService,
                _pedidoDroneRepository,
                _droneRepository,
                _pedidoRepository);
            var pedidoService = new PedidoService(droneService, _coordinateService);
            _coordinateService.GetKmDistance(Arg.Any<Point>(), Arg.Any<Point>())
                .Returns(10);
            _pedidoDroneRepository.RetornaPedidosEmAbertoAsync().Returns(SetupTests.GetPedidoDrones(StatusEnvio.AGUARDANDO));
            _droneRepository.RetornaDroneStatus(Arg.Any<int>())
                .Returns(SetupTests.GetDroneStatusDto());
            var drone = await pedidoService.DroneAtendePedido(SetupTests.GetPedido());
            Assert.True(drone != null);
        }

    }
}
