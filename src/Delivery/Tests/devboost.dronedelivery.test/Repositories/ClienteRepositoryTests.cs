using Castle.DynamicProxy.Internal;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.Infra.Data;
using NSubstitute;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Repositories
{
    public class ClienteRepositoryTests
    {

        private ClienteRepository GetRepository()
        {
            var data = SetupTests.GetClientes();
            var context = ContextProvider<core.domain.Entities.Cliente>.GetContext(data);
            return new ClienteRepository(context);

        }


        [Fact]
        public async Task TestSaveClient()
        {
            var context = ContextProvider<Cliente>.GetContext(null);
            var clienteRepository = new ClienteRepository(context);
            await clienteRepository.AddAsync(SetupTests.GetCliente(1));
            await context.SaveChangesAsync();
        }


        [Fact]
        public async Task GetCliente()
        {

            var cliente = SetupTests.GetCliente(2);
            var context = ContextProvider<Cliente>.GetContext(null);
            var clienteRepository = new ClienteRepository(context);
            await clienteRepository.AddAsync(cliente);
            var result = await clienteRepository.GetByIdAsync(cliente.Id);

            Assert.True(cliente.Equals(result));

        }

        [Fact]
        public async Task GetClientes()
        {

            var clientes = GetRepository().GetClientes();

            Assert.IsType(TypeUtil.GetTypeOrNull(new List<Cliente>()), clientes);
            Assert.True(clientes != null);
            
        }

    }
}
