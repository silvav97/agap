using Agap.Shared.Responses;

namespace Agap.Backemd.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAsync();

        Task<Response<T>> AddAsync(T entity);

        Task DeleteAsync(int id);

        Task<Response<T>> UpdateAsync(T entity);
    }
}
