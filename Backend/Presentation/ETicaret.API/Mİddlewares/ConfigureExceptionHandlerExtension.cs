using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;

namespace ETicaret.API.Mİddlewares
{
    public static class ConfigureExceptionHandlerExtension
    {
        public static void UseConfigureExceptionHandler<T>(this WebApplication application, ILogger<object> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError(contextFeature.Error.Message);

                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "Internal server error."
                        });

                    }
                });
            });
        }
    }
}
