using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Enums;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.Infra.Data;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Repositories
{
    public class PedidoDroneRepositoryTests
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDroneRepository _droneRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly PedidoDroneRepository _pedidoDroneRepository;
        private readonly DataContext _dataContext;
        private readonly ICommandExecutor<PedidoDrone> _commandExecutor;

        public PedidoDroneRepositoryTests()
        {
            var data = SetupTests.GetPedidoDrones(StatusEnvio.AGUARDANDO);
            _dataContext = ContextProvider<PedidoDrone>.GetContext(data);
            _commandExecutor = Substitute.For<ICommandExecutor<PedidoDrone>>();
            _pedidoRepository = Substitute.For<IPedidoRepository>();
            _droneRepository = Substitute.For<IDroneRepository>();
            _clienteRepository = Substitute.For<IClienteRepository>();
            _pedidoDroneRepository = new PedidoDroneRepository(_dataContext, _pedidoRepository,
                _droneRepository, _clienteRepository, _commandExecutor);
        }

        [Fact]
        public async Task RetornaPedidosEmAbertoTest()
        {
            _pedidoRepository.GetByIdAsync(Arg.Any<int>()).Returns(SetupTests.GetPedido());
            _clienteRepository.GetByIdAsync(Arg.Any<int>()).Returns(SetupTests.GetCliente());

            var pedidos = await _pedidoDroneRepository.RetornaPedidosEmAbertoAsync();
            Assert.True(pedidos.Count > 0);
        }

        [Fact]
        public async Task RetornaPedidosParaFecharAsync()
        {
            _pedidoRepository.GetByIdAsync(Arg.Any<int>()).Returns(SetupTests.GetPedido());
            _clienteRepository.GetByIdAsync(Arg.Any<int>()).Returns(SetupTests.GetCliente());

            var pedidos = await _pedidoDroneRepository.RetornaPedidosParaFecharAsync();
            Assert.True(pedidos.Count == 0);
        }

        [Fact]
        public async Task UpdatePedidoDroneAsyncTest()
        {
            var pedidos = _pedidoDroneRepository.UpdatePedidoDroneAsync(SetupTests.GetDroneStatusDto(), (double)10);

            Assert.True(pedidos.IsCompletedSuccessfully);


        }

    }
}
