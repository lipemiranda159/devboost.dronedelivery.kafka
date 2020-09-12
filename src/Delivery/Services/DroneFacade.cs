using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Facade
{
    public class DroneFacade : IDroneFacade
    {
        private readonly IDroneService _droneService;
        private readonly IDroneRepository _droneRepository;
        public DroneFacade(IDroneService droneService, IDroneRepository droneRepository)
        {
            _droneService = droneService;
            _droneRepository = droneRepository;
        }
        public List<StatusDroneDto> GetDroneStatus()
        {
            return _droneService.GetDroneStatus().ToList();
        }

        public async Task<Drone> SaveDroneAsync(Drone drone)
        {
            drone.SetPerformance();
            await _droneRepository.AddAsync(drone);

            return drone;
        }

    }
}
