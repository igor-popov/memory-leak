using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Events.Server.ProxyHelpers;

namespace Events.Server.Controllers
{
    [RoutePrefix("api/microservice")]
    public class MicroserviceController : ApiController
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "http://localhost:5004";
        private readonly ControllerProxyFactory _proxyFactory;

        public MicroserviceController()
        {
            _proxyFactory = new ControllerProxyFactory(_httpClient, _baseUrl);
        }

        [HttpGet]
        [Route("")]
        public string[] ReplyToPing()
        {
            var proxy = _proxyFactory.Create<IValues>();
            return proxy.Values();
        }
    }
}