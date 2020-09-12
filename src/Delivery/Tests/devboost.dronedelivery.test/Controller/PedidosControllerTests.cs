using devboost.dronedelivery.Api.Controllers;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Controller
{
    public class PedidosControllerTests
    {
        private readonly IPedidoFacade _pedidoFacade;
        private readonly IClienteRepository _clienteRepository;

        public PedidosControllerTests()
        {
            _pedidoFacade = Substitute.For<IPedidoFacade>();
            _clienteRepository = Substitute.For<IClienteRepository>();
        }
        [Fact]
        public async Task TestAssignDrone()
        {
            var pedidosController = new PedidosController(_pedidoFacade);
            await pedidosController.AssignDrone();
            await _pedidoFacade.Received().AssignDroneAsync();

        }
        [Fact]
        public async Task TestPostPedido()
        {
            _clienteRepository.GetByIdAsync(Arg.Any<int>()).
                Returns(SetupTests.GetCliente());

            var pedidosController = new PedidosController(_pedidoFacade);
            var pedido = await pedidosController.PostPedido(SetupTests.GetPedido());
            await _pedidoFacade.Received().CreatePedidoAsync(Arg.Any<Pedido>());

        }

    }
}
