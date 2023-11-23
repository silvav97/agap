using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.Repositories
{
    public interface ICropReportsRepository
    {
        Task<Response<IEnumerable<CropReport>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<Response<int>> AddAsync(int cropId);

    }
}