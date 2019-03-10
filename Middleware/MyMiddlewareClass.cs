using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware
{
    public class MyMiddlewareClass
    {
        RequestDelegate _next;

        public MyMiddlewareClass(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("My middleware classs is handling the request\n");
            await _next.Invoke(context);
            await context.Response.WriteAsync("My middleware classs has completed handling the request\n");
        }
    }

    public static class MyMiddleWareExtensions
    {
        public static IApplicationBuilder UseMyMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddlewareClass>();
        }
    }
}