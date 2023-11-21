namespace WebApiPrestamos.Middlewares
{
    public static class LoggingResponseHttpMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogginResponseHttp(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogginResponseHTTPMiddleware>();
        }
    }
    public class LogginResponseHTTPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogginResponseHTTPMiddleware> _logger;

        public LogginResponseHTTPMiddleware(
            RequestDelegate next,
            ILogger<LogginResponseHTTPMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        // Invoke o InvokeAsync

        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpoOriginalRespuesta = context.Response.Body;
                context.Response.Body = ms;
                await _next(context);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();

                ms.Seek(0, SeekOrigin.Begin);
                await ms.CopyToAsync(cuerpoOriginalRespuesta);

                context.Response.Body = cuerpoOriginalRespuesta;
                _logger.LogInformation(respuesta);
            }
        }
    }
}
