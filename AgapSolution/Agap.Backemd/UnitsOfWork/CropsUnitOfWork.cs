using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class CropsUnitOfWork : GenericUnitOfWork<Crop>, ICropsUnitOfWork
    {
        private readonly ICropsRepository _cropsRepository;

        public CropsUnitOfWork(IGenericRepository<Crop> repository, ICropsRepository cropsRepository) : base(repository)
        {
            _cropsRepository = cropsRepository;
        }

        public override async Task<Response<IEnumerable<Crop>>> GetAsync(PaginationDTO pagination) => await _cropsRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _cropsRepository.GetTotalPagesAsync(pagination);

        public override async Task<Response<Crop>> GetAsync(int id) => await _cropsRepository.GetAsync(id);

    }
}
