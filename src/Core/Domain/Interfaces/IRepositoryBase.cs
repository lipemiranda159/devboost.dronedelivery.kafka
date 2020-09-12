using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.core.domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> UpdateAsync(TEntity obj);
        Task RemoveAsync(TEntity obj);
        Task SaveAsync();
        void Dispose();
    }

}
