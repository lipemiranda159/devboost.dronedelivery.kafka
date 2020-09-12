using devboost.dronedelivery.core.domain.Enums;

namespace devboost.dronedelivery.core.domain.Extensions
{
    public static class StatusPedidoExtensions
    {
        public static bool IsSuccess(this EStatusPagamento statusPagamento)
        {
            return statusPagamento == EStatusPagamento.APROVADO;
        }

        public static bool EmAnalise(this EStatusPagamento statusPagamento)
        {
            return statusPagamento == EStatusPagamento.EM_ANALISE;
        }
    }
}
