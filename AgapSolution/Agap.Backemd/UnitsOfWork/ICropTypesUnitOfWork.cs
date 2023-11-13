using Agap.Shared.DTOs;
using Agap.Shared.Entities.Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public interface ICropTypesUnitOfWork
    {
        Task<Response<IEnumerable<CropType>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}