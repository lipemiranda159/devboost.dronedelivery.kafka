using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Entities;
using System.Threading.Tasks;

namespace devboost.dronedelivery.domain.Interfaces
{
    public interface IPedidoService
    {
        Task<DroneDto> DroneAtendePedido(Pedido pedido);
    }
}
