using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.Repositories
{
    public interface IPesticidesRepository
    {
        Task<Response<IEnumerable<Pesticide>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
