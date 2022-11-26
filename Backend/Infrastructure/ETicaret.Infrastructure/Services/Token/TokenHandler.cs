using ETicaret.Application.Abstractions.Token;
using ETicaret.Application.DTOs;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ETicaret.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public TokenDto CreateAccessToken(int second, AppUser user)
        {
            TokenDto tokenDto = new();
            var securityKey = SecurityKey(_configuration["Token:SecurityKey"]);
            var credentials = SigningCredentials(securityKey);

            tokenDto.Expiration = DateTime.UtcNow.AddSeconds(second);
            var token = new JwtSecurityToken(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: tokenDto.Expiration,
                claims: GetUserClaims(user),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
            );
            //not before bu token üretildiği anda devreye girsin demiş olduk bu süreyi manupule edebiliriz
            
            //token oluşturucu sınıfından bir örnek alalım burada 

            JwtSecurityTokenHandler tokenHandler = new();
            
            var resultToken = tokenHandler.WriteToken(token);
            tokenDto.AccessToken = resultToken;
            tokenDto.RefreshToken = CreateRefreshToken();

            return tokenDto;
        }
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

        private List<Claim> GetUserClaims(AppUser user)
        {
            var claimList = new List<Claim>();
            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            //var role = _roleManager.FindByNameAsync(userRole)?.Result;
            //var claims = _roleManager.GetClaimsAsync(role).Result.ToList();

            claimList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimList.Add(new Claim(ClaimTypes.Email, user.Email));
            claimList.Add(new Claim(ClaimTypes.Name, user.NameSurname));
            //claimList.Add(new Claim(ClaimTypes.Role, role.Name));
            //claimList.AddRange(claims.Select(c => new Claim(c.Type, c.Value)).Where(c => c.Type == "Permission").ToList());
            return claimList;
        }
        private SigningCredentials SigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }
        private SecurityKey SecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}
