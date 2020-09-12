using devboost.dronedelivery.domain.Enums;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test.Repositories
{
    public class PedidoRepositoryTests
    {
        private PedidoRepository GetRepository()
        {
            var data = SetupTests.GetPedidosList();
            var context = ContextProvider<core.domain.Entities.Pedido>.GetContext(data);
            return new PedidoRepository(context);

        }

        [Fact]
        public async Task GetPedidoTest()
        {

            var pedido = await GetRepository().GetByIdAsync(1);
            Assert.True(pedido != null);
        }
        [Fact]
        public void ObterPedidosTest()
        {
            var pedido = GetRepository().ObterPedidos((int)StatusPedido.AGUARDANDO);
            Assert.True(pedido != null);
        }
        [Fact]
        public async Task SavePedidosTest()
        {
            var pedidoTests = SetupTests.GetPedido();
            var repository = GetRepository();
            await repository.AddAsync(pedidoTests);
            var pedido = await repository.UpdateAsync(pedidoTests);
            Assert.True(pedido != null);
        }

        [Fact]
        public void PegaPedidoPendenteTest()
        {
            var repository = GetRepository();


            var pedido = repository.PegaPedidoPendenteAsync("1");


            Assert.True(pedido != null);
        }


    }
}
