using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;

namespace devboost.dronedelivery.domain.Extensions
{
    public static class PedidoExtensions
    {
        public static Point GetPoint(this Pedido pedido)
        {
            return new Point(pedido.Cliente.Latitude, pedido.Cliente.Longitude);
        }
    }
}
