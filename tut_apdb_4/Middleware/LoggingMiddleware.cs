using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace tut_apdb_4.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var info = getInformationAsync(httpContext);
            using (var fileStream = new FileStream("requestLog.txt", FileMode.Append))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(info.Result.ToString());
            }

            await _next(httpContext);

        }

        private async Task<Info> getInformationAsync(HttpContext context)
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                return new Info
                {
                    Body = body,
                    Endpoint = context.Request.Path.Value.ToString(),
                    Method = context.Request.Method.ToString(),
                    QueryString = context.Request.QueryString.ToString()
                };
            }
        }

    }
}

