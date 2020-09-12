using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.domain.Interfaces
{
    public interface IDroneService
    {
        Task<DroneStatusDto> GetAvailiableDroneAsync(double distance, Pedido pedido);
        List<StatusDroneDto> GetDroneStatus();

    }
}
