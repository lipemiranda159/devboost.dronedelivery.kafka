namespace devboost.dronedelivery.core.domain.Entities
{
    public sealed class StatusDroneDto
    {
        public int DroneId { get; set; }
        public bool Situacao { get; set; }
        public int PedidoId { get; set; }

        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
