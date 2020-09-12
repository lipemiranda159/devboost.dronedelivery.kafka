using devboost.dronedelivery.core.domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.pagamento.EF.Repositories
{
    public class PagamentoRepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected DataContext Context;

        public PagamentoRepositoryBase(DataContext context)
        {
            Context = context;
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            Context.Set<TEntity>().Add(obj);
            await Context.SaveChangesAsync();
            return obj;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return obj;
        }

        public async Task RemoveAsync(TEntity obj)
        {
            Context.Set<TEntity>().Remove(obj);
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {

        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
