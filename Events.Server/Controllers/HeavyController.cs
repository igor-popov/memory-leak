using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Events.Server.Controllers
{
    [RoutePrefix("api/heavy")]
    public class HeavyController : ApiController
    {
        [HttpGet]
        [Route("")]
        public List<MyDto> GetHeavy(bool very = false)
        {
            return GenerateList(very ? 20000 : 7000);
        }

        private List<MyDto> GenerateList(int count)
        {
            return Enumerable.Range(0, count).Select(i => new MyDto(i)).ToList();
        }
    }

    public class MyDto
    {
        public MyDto(int value)
        {
            Value = $"Some long line  corresponding to number {value}";
        }

        public string Value { get; }
    }
}