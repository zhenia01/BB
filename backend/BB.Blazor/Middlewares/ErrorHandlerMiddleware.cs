using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BB.Blazor.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    InvalidOperationException => (int) HttpStatusCode.BadRequest,
                    KeyNotFoundException => (int) HttpStatusCode.NotFound,
                    ArgumentOutOfRangeException => (int) HttpStatusCode.BadRequest,
                    _ => (int) HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(new {message = $"{error.InnerException?.Message} - {error.Message}"});
                context.Response.Redirect("/Error");
                await response.WriteAsync(result); 
            }
        }
    }
}