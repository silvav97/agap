using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public interface IProjectsUnitOfWork
    {
        Task<Response<IEnumerable<Project>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
