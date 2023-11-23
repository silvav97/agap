using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public interface ICropsUnitOfWork
    {
        Task<Response<IEnumerable<Crop>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<Response<IEnumerable<Crop>>> GetAllAsync();

        Task<Response<bool>> CloseCropAsync(int cropId);
    }
}