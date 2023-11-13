using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.Repositories
{
    public interface IUsersRepository
    {
        Task<Response<User>> GetAsync(string email);

        Task<Response<User>> GetAsync(Guid userId);

        Task<Response<IEnumerable<User>>> GetAsync(PaginationDTO pagination);

        Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
