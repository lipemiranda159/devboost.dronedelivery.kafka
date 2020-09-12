using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Interfaces;
using System.Collections.Generic;

namespace devboost.dronedelivery.domain.Interfaces.Repositories
{
    public interface IDroneRepository : IRepositoryBase<Drone>
    {
        Drone RetornaDrone();
        List<StatusDroneDto> GetDroneStatus();
        DroneStatusDto RetornaDroneStatus(int droneId);
    }
}
