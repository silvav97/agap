using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class PesticidesUnitOfWork : GenericUnitOfWork<Pesticide>, IPesticidesUnitOfWork
    {
        private readonly IPesticidesRepository _pesticidesRepository;

        public PesticidesUnitOfWork(IGenericRepository<Pesticide> repository, IPesticidesRepository pesticidesRepository) : base(repository)
        {
            _pesticidesRepository = pesticidesRepository;
        }

        public override async Task<Response<IEnumerable<Pesticide>>> GetAsync(PaginationDTO pagination) => await _pesticidesRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _pesticidesRepository.GetTotalPagesAsync(pagination);
    }
}
