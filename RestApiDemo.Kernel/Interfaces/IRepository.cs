using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiDemo.Kernel.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T> GetByIdAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<T> AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
