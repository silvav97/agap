using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class CropReportsUnitOfWork : GenericUnitOfWork<CropReport>, ICropReportsUnitOfWork
    {
        private readonly ICropReportsRepository _cropReportsRepository;

        public CropReportsUnitOfWork(IGenericRepository<CropReport> repository, ICropReportsRepository cropReportsRepository) : base(repository)
        {
            _cropReportsRepository = cropReportsRepository;
        }

        public override async Task<Response<IEnumerable<CropReport>>> GetAsync(PaginationDTO pagination) => await _cropReportsRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _cropReportsRepository.GetTotalPagesAsync(pagination);
    }
}