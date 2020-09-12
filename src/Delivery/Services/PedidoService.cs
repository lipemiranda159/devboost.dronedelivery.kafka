using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Entities;
using devboost.dronedelivery.domain.Extensions;
using devboost.dronedelivery.domain.Interfaces;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IDroneService _droneService;
        private readonly ICoordinateService _coordinateService;
        public PedidoService(IDroneService droneService, ICoordinateService coordinateService)
        {
            _droneService = droneService;
            _coordinateService = coordinateService;
        }

        public async Task<DroneDto> DroneAtendePedido(Pedido pedido)
        {
            var originPoint = new Point();

            var distance = _coordinateService.GetKmDistance(originPoint, pedido.GetPoint());

            var drone = await _droneService.GetAvailiableDroneAsync(distance, pedido);

            if (drone == null)
                return null;

            return new DroneDto(drone, distance);

        }

    }
}
