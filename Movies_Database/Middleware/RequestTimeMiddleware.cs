using System.Diagnostics;

namespace Movies_Database.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly Stopwatch _stopwatch;
        private readonly int _timeout = 4;
        private readonly ILogger<RequestTimeMiddleware> _logger;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var elapsedTime = _stopwatch.ElapsedMilliseconds;

            if(elapsedTime/1000 > _timeout)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedTime} ms";

                _logger.LogInformation(message);
            }
        }
    }
}
