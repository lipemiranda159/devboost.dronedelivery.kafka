using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Api.Controllers
{
    /// <summary>
    /// Pagamento controller
    /// </summary>
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoFacade _pagamentoFacade;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pagamentoFacade"></param>
        public PagamentoController(IPagamentoFacade pagamentoFacade)
        {
            _pagamentoFacade = pagamentoFacade;
        }
        /// <summary>
        /// Controller para recebimento de status de pagamento
        /// </summary>
        /// <param name="listaPagamentos"></param>
        /// <remarks>
        /// Exemplo:
        /// Post /api/pagamento
        /// [
        ///     {
        ///         "IdPagamento" : 1,
        ///         "Status": 1,
        ///         "Descricao":"ok"
        ///     }
        /// ]
        /// </remarks>
        [HttpPost]
        public async Task AtualizaLista(List<PagamentoStatusDto> listaPagamentos)
        {
            await _pagamentoFacade.ProcessaPagamentosAsync(listaPagamentos);
        }
    }
}
