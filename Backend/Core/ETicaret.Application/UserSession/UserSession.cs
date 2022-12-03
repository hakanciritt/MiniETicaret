using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ETicaret.Application.UserSession
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? UserId
        {
            get
            {
                var value = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value;
                return !string.IsNullOrEmpty(value) ? value : null;
            }
        }

        public string GetUserId => 
            _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(d => d.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
