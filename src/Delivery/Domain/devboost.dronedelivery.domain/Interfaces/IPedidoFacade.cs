using devboost.dronedelivery.core.domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.domain.Interfaces
{
    public interface IPedidoFacade
    {
        Task AssignDroneAsync();
        Task<Pedido> CreatePedidoAsync(Pedido pedido);

        Task<IEnumerable<Pedido>> ObterTodosPedidos();

    }
}
