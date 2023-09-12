using Agap.Shared.Responses;

namespace Agap.Backemd.Services
{
    public interface IApiService
    {
        Task<Response<T>> GetAsync<T>(string servicePrefix, string controller);
    }
}
