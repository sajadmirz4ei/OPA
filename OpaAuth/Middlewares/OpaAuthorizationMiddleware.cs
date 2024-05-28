using OpaAuth.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace OpaAuth.Middlewares
{
    public class OpaAuthorizationMiddleware
    {
        private readonly HttpClient _httpClient;
        private readonly RequestDelegate _next;
        private readonly string _opaUrl;

        public OpaAuthorizationMiddleware(RequestDelegate next, string opaUrl)
        {
            _httpClient = new HttpClient();
            _next = next;
            _opaUrl = opaUrl;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var isAuthenticated = context.User.Identity.IsAuthenticated;

            var opaRequest = new
            {
                input = new
                {
                    method = context.Request.Method,
                    path = context.Request.Path.Value,
                    user = context.User.Identity.IsAuthenticated ? context.User.Identity.Name : string.Empty,
                    role = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
                }
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(opaRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_opaUrl, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("OPA policy evaluation failed.");
                return;
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var opaResponse = JsonSerializer.Deserialize<OpaResponse>(responseBody);

            if (opaResponse.Result)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied.");
            }
        }
    }
}
