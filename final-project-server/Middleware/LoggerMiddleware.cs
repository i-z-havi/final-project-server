using System.Diagnostics;

namespace final_project_server.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch taskTimer = Stopwatch.StartNew();
            string path = "Path: " + context.Request.Path;
            string method = "Method: " + context.Request.Method;
            string origin = "Origin: " + context.Request.Headers["Origin"].FirstOrDefault();
            string timestamp = "Timestamp: " + DateTime.Now.ToString();

            await _next(context);

            taskTimer.Stop();
            long responseTime = taskTimer.ElapsedMilliseconds;
            int response = context.Response.StatusCode;
            if (response>=400)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine(path);
            Console.WriteLine(method);
            Console.WriteLine(origin);
            Console.WriteLine(timestamp);
            Console.ForegroundColor= ConsoleColor.White;
        }
    }
}
