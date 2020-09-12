using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.domain.Interfaces.Repositories
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        List<Pedido> ObterPedidos(int situacao);
        Task<Pedido> PegaPedidoPendenteAsync(string GatewayId);

        Task<IEnumerable<Pedido>> ObterTodosPedidos();

        void SetState(Pedido pedido, EntityState entityState);
    }
}
