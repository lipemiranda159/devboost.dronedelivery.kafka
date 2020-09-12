using devboost.dronedelivery.Api.Controllers;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Controller
{
    public class ClientControllerTests
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientControllerTests()
        {
            _clienteRepository = Substitute.For<IClienteRepository>();
        }

        [Fact]
        public async Task TestGetClientes()
        {
            var clientController = new ClienteController(_clienteRepository);
            _clienteRepository.GetAllAsync().Returns(SetupTests.GetClientes());
            var clientes = await clientController.Get();

            Assert.True(clientes.Value.Count() == 1);

        }
        [Fact]
        public async Task TestPostCliente()
        {
            var clientController = new ClienteController(_clienteRepository);
            var clienteSetup = SetupTests.GetCliente();
            var client = await clientController.Post(clienteSetup);
            Assert.True(client.Value.Equals(clienteSetup));
            await _clienteRepository.Received().AddAsync(Arg.Any<Cliente>());


        }


    }
}
