using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Middleware
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello from component one!\n");
                await next.Invoke();
                await context.Response.WriteAsync("Hello from the component one RESPONSE!\n");
            });

            app.UseMyMiddleWare();

            app.Map("/mymapbranch", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Greeting from my MAP!\n");
                });
            });

            app.MapWhen(context => context.Request.Query.ContainsKey("querybranch"), (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    await context.Response.WriteAsync("You have arrived at your Map When Branch!");
                });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!\n");
            });
        }
    }
}
