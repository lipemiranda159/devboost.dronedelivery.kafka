using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.pagamento.domain.Interfaces;
using devboost.dronedelivery.pagamento.EF.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace devboost.dronedelivery.pagamento.Api.Controllers
{
    /// <summary>
    /// Controller com ações de pagamento
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ExcludeFromCodeCoverage]
    public class PagamentosController : ControllerBase
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IPagamentoFacade _pagamentoFacade;

        public PagamentosController(IPagamentoRepository pagamentoRepository, IPagamentoFacade pagamentoFacade)
        {
            _pagamentoRepository = pagamentoRepository;
            _pagamentoFacade = pagamentoFacade;
        }
        /// <summary>
        /// Retorna Pagamentos cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamento()
        {
            var pagamentos = await _pagamentoRepository.GetAllAsync();
            return pagamentos.ToList();
        }
        /// <summary>
        /// Endpoint para finalização de requisições de pagamento
        /// Ao ser chamado este endpoint vai gerar um status para o pagamento e chamar a api de drones
        /// para dizer se o pagamento ocorreu com sucesso ou com falha
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("VerificarStatusPagamentos")]
        public async Task<ActionResult<IEnumerable<PagamentoStatusDto>>> VerificarStatusPagamentos()
        {
            try
            {
                var consulta = await _pagamentoFacade.VerificarStatusPagamentos();

                if (consulta == null)
                    return BadRequest("Não foi possivel comunicar com a API de Delivery!");

                return consulta.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Retorna pagamento por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(int id)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return pagamento;
        }
        /// <summary>
        /// Atualiza dados do pagamento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pagamento"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamento(int id, Pagamento pagamento)
        {
            if (id != pagamento.Id)
            {
                return BadRequest();
            }

            _pagamentoRepository.SetState(pagamento, EntityState.Modified);

            try
            {
                await _pagamentoRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _pagamentoRepository.PagamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Recebe a requecisção de pagamento
        /// </summary>
        /// <param name="pagamento"></param>
        /// <remarks>
        /// Exemplo:
        /// 
        /// POST api/Pagamento
        /// {
        ///     "DadosPagamentos":
        ///         [
        ///             {
        ///                 "Id":0,
        ///                 "Dados":"num_cartao:0000000000000000,validade:08/28,Codigo:123,Nome:Joao"
        ///             }
        ///         ],
        ///     "TipoPagamento":0,
        ///     "Descricao":"teste"
        ///}
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Pagamento>> PostPagamento(PagamentoCreateDto pagamento)
        {
            try
            {
                var newPagamento = await _pagamentoFacade.CriarPagamento(pagamento);
                return Ok(newPagamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
