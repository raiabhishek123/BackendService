using backend_service.Models;

namespace backend_service.Services;

public interface ILoginService
{
    Task<string> LoginAsync(Login login);
    Task<string> RefreshTokenAsync();
}