using System.Text;

namespace OpaAuth.Middlewares
{
    public class CustomLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggingMiddleware> _logger;

        public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestBody = await ReadRequestBodyAsync(context);
            var originalResponseBodyStream = context.Response.Body;
            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;
                await _next(context);

                var responseBody = await ReadResponseBodyAsync(context);
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);

                _logger.LogInformation("Request: {method} {url} {headers} {body}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.Headers,
                    requestBody);

                _logger.LogInformation("Response: {statusCode} {headers} {body}",
                    context.Response.StatusCode,
                    context.Response.Headers,
                    responseBody);
            }
        }
        private async Task<string> ReadRequestBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            var body = await new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true).ReadToEndAsync();
            context.Request.Body.Position = 0;

            return body;
        }

        private async Task<string> ReadResponseBodyAsync(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            return body;
        }
    }
}
