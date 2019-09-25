using System;
using System.Web.Http;

namespace Events.Server.Controllers
{
    [RoutePrefix("api/ping")]
    public class PingController: ApiController
    {
        [HttpGet]
        [Route("")]
        public string ReplyToPing()
        {
            return $"Server responded {DateTime.UtcNow:s}";
        }
    }
}