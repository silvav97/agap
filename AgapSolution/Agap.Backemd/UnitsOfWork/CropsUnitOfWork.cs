using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.UnitsOfWork
{
    public class CropsUnitOfWork : GenericUnitOfWork<Crop>, ICropsUnitOfWork
    {
        private readonly ICropsRepository _cropsRepository;

        private readonly ICropReportsRepository _cropReportsRepository;

        public CropsUnitOfWork(IGenericRepository<Crop> repository, ICropsRepository cropsRepository, ICropReportsRepository cropReportsRepository) : base(repository)
        {
            _cropsRepository = cropsRepository;
            _cropReportsRepository = cropReportsRepository;
        }

        public override async Task<Response<IEnumerable<Crop>>> GetAsync(PaginationDTO pagination) => await _cropsRepository.GetAsync(pagination);

        public override async Task<Response<IEnumerable<Crop>>> GetAllAsync() => await _cropsRepository.GetAllAsync();


        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _cropsRepository.GetTotalPagesAsync(pagination);


        public async Task<Response<bool>> CloseCropAsync(int cropId)
        {
            var response = await _cropsRepository.CloseCropAsync(cropId);

            if (response.WasSuccess)
            {
                await _cropReportsRepository.AddAsync(cropId);
            }

            return response;
        }
    }
}
