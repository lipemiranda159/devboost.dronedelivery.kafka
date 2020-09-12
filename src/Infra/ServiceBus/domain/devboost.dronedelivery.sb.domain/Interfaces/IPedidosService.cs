using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.domain.Interfaces
{
    public interface IPedidosService
    {
        Task ProcessPedidoAsync(string token, string pedido);
    }
}
