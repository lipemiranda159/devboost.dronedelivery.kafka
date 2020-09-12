using devboost.dronedelivery.core.domain;

namespace devboost.dronedelivery.domain.Entities
{
    public sealed class DroneDto
    {
        public DroneDto(DroneStatusDto droneStatus, double distancia)
        {
            DroneStatus = droneStatus;
            Distancia = distancia;
        }
        public DroneStatusDto DroneStatus { get; set; }

        public double Distancia { get; set; }

    }

}
