using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;


namespace Agap.Backemd.Repositories
{
    public interface IProjectReportsRepository
    {
        Task<Response<IEnumerable<ProjectReport>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
