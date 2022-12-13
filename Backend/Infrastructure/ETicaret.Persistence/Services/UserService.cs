using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.DTOs.User;
using ETicaret.Application.Exceptions;
using ETicaret.Application.Helpers;
using ETicaret.Application.ViewModels;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ETicaret.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUserDto model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task<List<AppUser>> GetAllUsers(PagedRequest pagination)
        {
            return await _userManager.Users.Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToListAsync();
        }

        public async Task UpdatePassword(string userId, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is not null)
            {
                resetToken = resetToken.UrlDecode();
                var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }

                throw new UserFriendlyException("Doğrulama sırasında bir hata meydana geldi.");
            }
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new NotFoundUserException($"{user?.UserName} adlı kullanıcı bulunamadı");
            }

        }
    }
}
