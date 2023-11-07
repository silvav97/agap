using Agap.Shared.DTOs;
using Agap.Shared.Entities.Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.Repositories
{
    public interface ICropTypesRepository
    {
        Task<Response<IEnumerable<CropType>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
