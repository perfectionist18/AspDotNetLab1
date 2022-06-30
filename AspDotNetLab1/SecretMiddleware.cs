using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspDotNetLab1
{
    public class SecretMiddleware
    {
        private readonly RequestDelegate _next;

        public SecretMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string path = httpContext.Request.Path.Value.ToLower();

            if (Regex.IsMatch(path, @"/secret-[0-9]+"))
            {
                await httpContext.Response.WriteAsync("Secret Page");
            }
            else
            {
                httpContext.Response.StatusCode = 404;
            }
        }
    }

    public static class SecretMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecretMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecretMiddleware>();
        }
    }
}
