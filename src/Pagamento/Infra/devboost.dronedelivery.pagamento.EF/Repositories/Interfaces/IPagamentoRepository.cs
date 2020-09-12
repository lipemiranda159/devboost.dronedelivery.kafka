using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.pagamento.EF.Repositories.Interfaces
{
    public interface IPagamentoRepository : IRepositoryBase<Pagamento>
    {
        Task<List<Pagamento>> GetPagamentosEmAnaliseAsync();
        void SetState(Pagamento pagamento, EntityState entityState);
        Task<bool> PagamentoExists(int id);

    }
}
