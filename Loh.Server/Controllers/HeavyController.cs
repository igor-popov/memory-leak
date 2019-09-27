using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Loh.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeavyController : ControllerBase
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("")]
        public ActionResult<List<MyDto>> GetHeavy(bool very = false)
        {
            return Ok(GenerateList(very ? 20000 : 7000));
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
