using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.domain.Enums;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.felipe.Facade;
using devboost.dronedelivery.Infra.Data;
using devboost.dronedelivery.test.Repositories;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test
{
    public class CriarPedidoUnitTest
    {

        private readonly DataContext _dataContext;
        private readonly IPedidoService _pedidoService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDroneRepository _droneRepository;
        private readonly IPagamentoServiceFactory _pagamentoServiceFactory;
        private readonly IPagamentoServico _pagamentoServico;
        private readonly IPedidoDroneRepository _pedidoDroneRepository;

        public CriarPedidoUnitTest()
        {
            _dataContext = ContextProvider<Pedido>.GetContext(null);
            _pedidoService = Substitute.For<IPedidoService>();
            _clienteRepository = Substitute.For<IClienteRepository>();
            _pedidoRepository = Substitute.For<IPedidoRepository>();
            _pedidoDroneRepository = Substitute.For<IPedidoDroneRepository>();

            _droneRepository = Substitute.For<IDroneRepository>();
            _pagamentoServiceFactory = Substitute.For<IPagamentoServiceFactory>();
            _pagamentoServico = Substitute.For<IPagamentoServico>();
        }

        [Fact]
        public async Task CriarPedido()
        {
            var _pedidoRepository = new MockPedidoRepository(_dataContext);

            const string clientePassword = "12543GTFrd@65";
            const int pedidoId = 1;

            var cliente = new Cliente
            {
                Id = pedidoId,
                Latitude = -23.5880684,
                Longitude = -46.6564195,
                Nome = "João Silva Antunes",
                UserId = "joaoantunes",
                Password = clientePassword
            };

            var pedido = new Pedido
            {
                Peso = 100,
                Situacao = (int)StatusPedido.AGUARDANDO,
                DataHoraInclusao = DateTime.Now,
                DataHoraFinalizacao = DateTime.Now,
                Cliente = cliente
            };

            await _pedidoRepository.AddAsync(pedido);

            Assert.Single(_pedidoRepository.ObterPedidos(pedidoId));
            var pedidoTets = await _pedidoRepository.GetByIdAsync(pedidoId);
            Assert.Equal(pedidoTets.Cliente.Password, clientePassword);
        }

        [Fact]
        public async Task CriarPedidoAsync()
        {

            var pedido = new PedidoFacade(_pedidoService, _clienteRepository, _pedidoRepository, _droneRepository, _pagamentoServiceFactory, _pedidoDroneRepository);

            _clienteRepository.GetByIdAsync(Arg.Any<int>()).Returns(SetupTests.GetCliente());
            _pagamentoServiceFactory.GetPagamentoServico(ETipoPagamento.CARTAO).Returns(_pagamentoServico);
            _pagamentoServico.RequisitaPagamento(Arg.Any<Pagamento>()).Returns(SetupTests.GetPagamento());

            var result = await pedido.CreatePedidoAsync(SetupTests.GetPedido());

            Assert.NotNull(result);
        }
    }
}
