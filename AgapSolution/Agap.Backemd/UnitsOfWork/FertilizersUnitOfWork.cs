using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class FertilizersUnitOfWork : GenericUnitOfWork<Fertilizer>, IFertilizersUnitOfWork
    {
        private readonly IFertilizersRepository _fertilizersRepository;

        public FertilizersUnitOfWork(IGenericRepository<Fertilizer> repository, IFertilizersRepository fertilizersRepository) : base(repository)
        {
            _fertilizersRepository = fertilizersRepository;
        }

        public override async Task<Response<IEnumerable<Fertilizer>>> GetAsync(PaginationDTO pagination) => await _fertilizersRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _fertilizersRepository.GetTotalPagesAsync(pagination);
    }
}
