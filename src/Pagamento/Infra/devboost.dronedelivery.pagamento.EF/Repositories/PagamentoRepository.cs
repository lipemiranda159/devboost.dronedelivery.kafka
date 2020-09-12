using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Extensions;
using devboost.dronedelivery.pagamento.EF.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devboost.dronedelivery.pagamento.EF.Repositories
{
    public class PagamentoRepository : PagamentoRepositoryBase<Pagamento>, IPagamentoRepository
    {
        private readonly DataContext _context;
        public PagamentoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Pagamento>> GetPagamentosEmAnaliseAsync()
        {
            return await _context.Pagamento.Where(w => w.StatusPagamento.EmAnalise()).ToListAsync();
        }

        public async Task<bool> PagamentoExists(int id)
        {
            var pagamento = await GetByIdAsync(id);
            return pagamento != null;
        }


        public void SetState(Pagamento pagamento, EntityState entityState)
        {
            _context.Entry(pagamento).State = EntityState.Modified;
        }
    }
}
