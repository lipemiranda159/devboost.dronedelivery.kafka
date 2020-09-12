using devboost.dronedelivery.core.domain.Entities;
using System.Threading.Tasks;

namespace devboost.dronedelivery.domain.Interfaces
{
    public interface IPagamentoServico
    {
        Task<Pagamento> RequisitaPagamento(Pagamento pagamento);
    }
}
