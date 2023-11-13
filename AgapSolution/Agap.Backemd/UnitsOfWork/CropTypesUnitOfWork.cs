using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities.Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class CropTypesUnitOfWork : GenericUnitOfWork<CropType>, ICropTypesUnitOfWork
    {
        private readonly ICropTypesRepository _cropTypesRepository;

        public CropTypesUnitOfWork(IGenericRepository<CropType> repository, ICropTypesRepository cropTypesRepository) : base(repository)
        {
            _cropTypesRepository = cropTypesRepository;
        }

        public override async Task<Response<IEnumerable<CropType>>> GetAsync(PaginationDTO pagination) => await _cropTypesRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _cropTypesRepository.GetTotalPagesAsync(pagination);
    }
}