using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Extensions;
using devboost.dronedelivery.domain.Enums;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Facade
{
    public class PagamentoFacade : IPagamentoFacade
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PagamentoFacade(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task ProcessaPagamentosAsync(List<PagamentoStatusDto> pagamentoStatus)
        {
            foreach (var item in pagamentoStatus)
            {
                var pedido = await _pedidoRepository.PegaPedidoPendenteAsync(item.IdPagamento.ToString());

                if (item.Status.IsSuccess())
                {
                    pedido.Situacao = (int)StatusPedido.AGUARDANDO;
                    pedido.Pagamento.StatusPagamento = item.Status;
                    _pedidoRepository.SetState(pedido, Microsoft.EntityFrameworkCore.EntityState.Modified);
                }
            }

            await _pedidoRepository.SaveAsync();
        }
    }
}
