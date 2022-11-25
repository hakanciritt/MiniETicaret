using ETicaret.Application.DTOs;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(int second,AppUser user);
        string CreateRefreshToken();

    }
}
