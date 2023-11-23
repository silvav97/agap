using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.Repositories
{
    public interface ICropsRepository
    {
        Task<Response<IEnumerable<Crop>>> GetAsync(PaginationDTO pagination);
        Task<Response<IEnumerable<Crop>>> GetAllAsync();

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
