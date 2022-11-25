using ETicaret.Application.DTOs;

namespace ETicaret.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<TokenDto> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<TokenDto> RefreshTokenLoginAsync(string refreshToken);
    }
}
