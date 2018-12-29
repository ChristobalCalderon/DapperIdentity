using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace WebApp.MiddleWare
{
    public class CORSMiddleware
    {
        private readonly RequestDelegate _next;

        public CORSMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var headers = context.Response.Headers;

            // OPTIONS is only used for Preflight requests
            if (context.Request.Method == "OPTIONS")
            {
                var requestHeaders = context.Request.Headers;
                headers.Add("Access-Control-Allow-Origin", getAllowedOrigin(requestHeaders));
                headers.Add("Access-Control-Allow-Methods", new string[] { "GET", "PUT", "POST", "DELETE" });
                headers.Add("Access-Control-Allow-Headers", getAllowedHeaders(requestHeaders));

                return;
            }
            else
            {
                headers.Add("Access-Control-Allow-Origin", "*");
                headers.Add("Access-Control-Expose-Headers", new string[] {
                    "Status",
                    "Status Code"
                    });
                headers.Add("Access-Control-Allow-Headers", new string[] {
                    "Content-Type"
                });
                await _next.Invoke(context);
            }

            StringValues getAllowedOrigin(IHeaderDictionary requestHeaders)
            {
                StringValues origin = "";
                if (requestHeaders.TryGetValue("Origin", out origin))
                {
                    return origin;
                }
                else
                {
                    return "*";
                }
            }

            StringValues getAllowedHeaders(IHeaderDictionary requestHeaders)
            {
                StringValues allowHeaders = "";
                if (requestHeaders.TryGetValue("Access-Control-Request-Headers", out allowHeaders))
                {
                    return allowHeaders;
                }
                else
                {
                    return new string[] { "Content-Type", };
                }
            }
        }
    }
    public static class CORSMiddlewareExtensions
    {
        public static IApplicationBuilder UseCORSMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CORSMiddleware>();
        }
    }
}
