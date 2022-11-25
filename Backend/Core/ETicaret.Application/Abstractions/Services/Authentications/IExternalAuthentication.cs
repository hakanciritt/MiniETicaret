using ETicaret.Application.DTOs;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<TokenDto> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime);
    }
}
