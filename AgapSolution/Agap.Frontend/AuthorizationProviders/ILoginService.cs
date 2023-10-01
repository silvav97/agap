namespace Agap.Frontend.AuthorizationProviders
{
    public interface ILoginService
    {
        Task LoginAsync(string token);

        Task LogoutAsync();
    }
}