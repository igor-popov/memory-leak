using System;
using System.Threading.Tasks;
using System.Web.Http;
using Events.Server.Middlewares;
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
                var configuration = new HttpConfiguration();


                configuration.MapHttpAttributeRoutes();

                //configuration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
                configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

                app.Use(new MagicMiddleware());

                app.UseWebApi(configuration);

                app.Map("", cfg => cfg.Run(async ctx =>
                {
                    ctx.Response.StatusCode = 200;
                    await ctx.Response.WriteAsync("You are here");
                }));
            }
            catch(Exception ex)
            {
                ReturnExceptionOnAnyCall(app, ex);
            }
        }

        private static void ReturnExceptionOnAnyCall(IAppBuilder app, Exception ex)
        {
            app.Use((context, next) =>
            {
                context.Response.StatusCode = 500;
                context.Response.ReasonPhrase = "Unexpected error";
                context.Response.Write(ex.ToString());
                context.Response.ContentType = "text/plain; charset=UTF-8";
                return Task.CompletedTask;
            });
        }
    }
}