using System;
using System.ComponentModel.DataAnnotations;

namespace devboost.dronedelivery.core.domain.Entities
{
    public class PedidoDrone
    {
        [Key]
        public int Id { get; set; }

        public int DroneId { get; set; }

        public Drone Drone { get; set; }

        public int PedidoId { get; set; }

        public Pedido Pedido { get; set; }

        public double Distancia { get; set; }

        public int StatusEnvio { get; set; }
        public DateTime DataHoraFinalizacao { get; set; }
    }
}
