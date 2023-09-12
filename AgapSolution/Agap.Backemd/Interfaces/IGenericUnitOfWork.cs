using Agap.Shared.Responses;

namespace Agap.Backemd.Interfaces
{
    public interface IGenericUnitOfWork<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();

        Task<Response<T>> AddAsync(T model);

        Task<Response<T>> UpdateAsync(T model);

        Task DeleteAsync(int id);

        Task<T> GetAsync(int id);
    }
}
