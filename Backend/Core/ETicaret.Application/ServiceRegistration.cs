using ETicaret.Application.UserSession;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserSession, UserSession.UserSession>();
            services.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
