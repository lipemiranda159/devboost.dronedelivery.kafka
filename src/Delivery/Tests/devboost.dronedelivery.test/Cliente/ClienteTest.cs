using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.Infra.Data;
using devboost.dronedelivery.test.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test
{
    public class ClienteTest
    {
        private readonly IClienteRepository _repository;
        private readonly DataContext _context;
        public ClienteTest()
        {
            _context = ContextProvider<Cliente>.GetContext(SetupTests.GetClientes());
            _repository = new ClienteRepositoryFake(_context);
        }

        [Fact]
        public void CriarCliente()
        {
            const double latitude = 23.5741381;
            const double longitude = 46.6610177;
            const string nome = "Marco";
            const string password = "AdminAPIDrone01!";

            var cliente = new Cliente()
            {
                Latitude = latitude,
                Longitude = longitude,
                Nome = nome,
                Password = password
            };

            Assert.Equal(latitude, cliente.Latitude);
            Assert.Equal(longitude, cliente.Longitude);
            Assert.Equal(nome, cliente.Nome);
            Assert.Equal(password, cliente.Password);

        }

        [Fact]
        public async Task GetCliente()
        {
            const int clienteId = 1;

            var cliente = await _repository.GetByIdAsync(clienteId);

            Assert.IsType<Cliente>(cliente);
            Assert.Equal(clienteId, cliente.Id);

        }

        [Fact]
        public void GetClientes()
        {
            var clientes = _repository.GetClientes();

            Assert.IsType<List<Cliente>>(clientes);
            Assert.Equal(5, clientes.Count());
        }

        [Fact]
        public async Task Save()
        {

            var cliente = SetupTests.GetCliente(2);
            cliente = await _repository.AddAsync(cliente);
            var result = await _repository.UpdateAsync(cliente);

            Assert.True(cliente.Equals(result));

        }

    }
}
