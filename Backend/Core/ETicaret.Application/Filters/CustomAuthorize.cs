using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ETicaret.Application.Filters
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string[] _permissions { get; set; }
        public CustomAuthorize() { }
        public CustomAuthorize(params string[] permissions)
        {
            _permissions = permissions;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims.Select(c => c.Value).ToList();
            
            if (_permissions?.Length > 0)
            {
                foreach (var permission in _permissions.ToList())
                {
                    if (claims.Contains(permission)) return;
                    else
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Result = new UnauthorizedObjectResult(new
                        { error = $"{permission} yetkisine sahip olmadığınız için giriş yapamazsınız." });
                    }
                }
            }
            else
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new UnauthorizedObjectResult(new { error = "Erişim engellendi." });
                    return;
                }
            }
        }
    }
}
