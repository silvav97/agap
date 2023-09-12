using Agap.Backemd.Interfaces;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericUnitOfWork(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<Response<T>> AddAsync(T model) => await _repository.AddAsync(model);

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<IEnumerable<T>> GetAsync() => await _repository.GetAsync();

        public async Task<T> GetAsync(int id) => await _repository.GetAsync(id);

        public async Task<Response<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);
    }
}
