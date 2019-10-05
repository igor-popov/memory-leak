using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DynamicAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicroserviceController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:5005";
        private readonly ControllerProxyFactory _proxyFactory;

        public MicroserviceController(IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient();
            _httpClient = httpClient;
            _proxyFactory = new ControllerProxyFactory(httpClient, _baseUrl);
        }

        // GET api/values
        [Route("")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var proxy = _proxyFactory.Create<IExternalMicroservice>();
            return proxy.Values();
        }

        [Route("directly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetDirectly()
        {
            string url = $"{_baseUrl}/api/values";
            return await Get(url);
        }

        private async Task<string[]> Get(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string resultAsString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string[]>(resultAsString);
        }
    }
}
