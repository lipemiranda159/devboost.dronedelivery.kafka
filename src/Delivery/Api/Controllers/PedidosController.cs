using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Api.Controllers
{
    /// <summary>
    /// Controller com ações de pedidos
    /// </summary>
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoFacade _pedidoFacade;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pedidoFacade"></param>
        public PedidosController(IPedidoFacade pedidoFacade)
        {
            _pedidoFacade = pedidoFacade;
        }
        /// <summary>
        /// Direciona pedidos para drones
        /// </summary>
        /// <returns></returns>
        [HttpPost("assign-drone")]
        public async Task<ActionResult> AssignDrone()
        {
            await _pedidoFacade.AssignDroneAsync();
            return Ok();
        }
        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <remarks>
        /// Exemplo:
        /// 
        ///     POST /api/pedido
        ///     {
        ///         "clienteId": 1,
        ///         "peso": 10,
        ///         "situacao": 0,
        ///         "pagamento": {
        ///         "dadosPagamentos":
        ///         [
        ///             {
        ///                 "Id":0,
        ///                 "Dados":"num_cartao:0000000000000000,validade:08/28,Codigo:123,Nome:Joao"
        ///             }
        ///         ],
        ///         "tipoPagamento": 0,
        ///         "statusPagamento": 0,
        ///         "descricao": "teste"
        ///         }
        ///     }
        ///    
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            return await _pedidoFacade.CreatePedidoAsync(pedido);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            var result = await _pedidoFacade.ObterTodosPedidos();
            return Ok(result);
        }
    }
}
