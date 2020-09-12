using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.pagamento.domain.Interfaces
{
    public interface IPagamentoFacade
    {
        Task<IEnumerable<PagamentoStatusDto>> VerificarStatusPagamentos();

        Task<Pagamento> CriarPagamento(PagamentoCreateDto pagamento);

    }
}
