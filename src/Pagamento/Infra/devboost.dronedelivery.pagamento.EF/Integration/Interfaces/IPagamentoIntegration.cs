using devboost.dronedelivery.core.domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.pagamento.EF.Integration.Interfaces
{
    public interface IPagamentoIntegration
    {
        Task<bool> ReportarResultadoAnalise(List<PagamentoStatusDto> listaPagamentos);
    }
}
