using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Intern.NTQ.Library.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandle(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {

                    };
                });
            });
        }
    }
}
