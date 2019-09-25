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
            var configuration = new HttpConfiguration();

            configuration.MapHttpAttributeRoutes();

            app.UseWebApi(configuration);
        }
    }
}