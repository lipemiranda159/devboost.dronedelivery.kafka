using devboost.dronedelivery.core.domain.Enums;

namespace devboost.dronedelivery.domain.Interfaces
{
    public interface IPagamentoServiceFactory
    {
        IPagamentoServico GetPagamentoServico(ETipoPagamento tipoPagamento);
    }
}
