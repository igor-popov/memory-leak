using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly:OwinStartup(typeof(Events.Server.Startup))]
namespace Events.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {

                app.Use(async (ctx, next) =>
                {
                    ctx.Response.StatusCode = 200;
                    await ctx.Response.WriteAsync("You are here");
                });

                return;
                var configuration = new HttpConfiguration();


                configuration.MapHttpAttributeRoutes();

                configuration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });



                app.Map("/", cfg => cfg.Run(async ctx =>
                {
                    ctx.Response.StatusCode = 200;
                    await ctx.Response.WriteAsync("You are here");
                }));

                app.UseWebApi(configuration);
            }
            catch(Exception ex)
            {
                app.Use((context, next) =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ReasonPhrase = "Unexpected error";
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(ex.ToString());
                    context.Response.Body.Write(bytes, 0, bytes.Length);
                    context.Response.ContentType = "text/plain; charset=UTF-8";
                    return Task.CompletedTask;
                });
            }
            
        }
    }
}