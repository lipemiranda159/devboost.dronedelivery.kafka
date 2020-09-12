using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.domain.Interfaces.Repositories
{
    public interface IPedidoDroneRepository : IRepositoryBase<PedidoDrone>
    {
        Task UpdatePedidoDroneAsync(DroneStatusDto drone, double distancia);
        Task<List<PedidoDrone>> RetornaPedidosEmAbertoAsync();
        Task<List<PedidoDrone>> RetornaPedidosParaFecharAsync();
    }
}
