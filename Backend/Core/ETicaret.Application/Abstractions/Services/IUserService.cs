using ETicaret.Application.DTOs.User;
using ETicaret.Application.ViewModels;
using ETicaret.Domain.Entities;

namespace ETicaret.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUserDto model);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task<List<AppUser>> GetAllUsers(PagedRequest pagination);
    }
}
